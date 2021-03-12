﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace AjaxControlToolkit
{
	// Token: 0x0200001D RID: 29
	public static class ScriptObjectBuilder
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x000038D8 File Offset: 0x00001AD8
		public static IEnumerable<ScriptReference> GetScriptReferences(Type type)
		{
			return ScriptObjectBuilder.GetScriptReferences(type, false);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000038E4 File Offset: 0x00001AE4
		public static IEnumerable<ScriptReference> GetScriptReferences(Type type, bool ignoreStartingTypeReferences)
		{
			List<ScriptObjectBuilder.ResourceEntry> scriptReferencesInternal = ScriptObjectBuilder.GetScriptReferencesInternal((ignoreStartingTypeReferences && null != type) ? type.BaseType : type, new Stack<Type>());
			return ScriptObjectBuilder.ScriptReferencesFromResourceEntries(scriptReferencesInternal);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003917 File Offset: 0x00001B17
		public static IEnumerable<string> GetCssReferences(Control control)
		{
			return ScriptObjectBuilder.GetCssReferences(control, control.GetType(), new Stack<Type>());
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000392C File Offset: 0x00001B2C
		public static void RegisterCssReferences(Control control)
		{
			foreach (string href in ScriptObjectBuilder.GetCssReferences(control))
			{
				HtmlLink htmlLink = new HtmlLink();
				htmlLink.Href = href;
				htmlLink.Attributes.Add("type", "text/css");
				htmlLink.Attributes.Add("rel", "stylesheet");
				control.Page.Header.Controls.Add(htmlLink);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000039C0 File Offset: 0x00001BC0
		public static string ExecuteCallbackMethod(Control control, string callbackArgument)
		{
			Type type = control.GetType();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(callbackArgument) as Dictionary<string, object>;
			string text = (string)dictionary["name"];
			object[] array = (object[])dictionary["args"];
			string clientState = (string)dictionary["state"];
			IClientStateManager clientStateManager = control as IClientStateManager;
			if (clientStateManager != null && clientStateManager.SupportsClientState)
			{
				clientStateManager.LoadClientState(clientState);
			}
			object value = null;
			string text2 = null;
			try
			{
				MethodInfo method = type.GetMethod(text, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
				if (method == null)
				{
					throw new MissingMethodException(type.FullName, text);
				}
				ParameterInfo[] parameters = method.GetParameters();
				ExtenderControlMethodAttribute extenderControlMethodAttribute = (ExtenderControlMethodAttribute)Attribute.GetCustomAttribute(method, typeof(ExtenderControlMethodAttribute));
				if (extenderControlMethodAttribute == null || !extenderControlMethodAttribute.IsScriptMethod || array.Length != parameters.Length)
				{
					throw new MissingMethodException(type.FullName, text);
				}
				object[] array2 = new object[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					if (array[i] != null)
					{
						array2[i] = Convert.ChangeType(array[i], parameters[i].ParameterType, CultureInfo.InvariantCulture);
					}
				}
				value = method.Invoke(control, array2);
			}
			catch (Exception innerException)
			{
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				text2 = innerException.GetType().FullName + ":" + innerException.Message;
			}
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			if (text2 == null)
			{
				dictionary2["result"] = value;
				if (clientStateManager != null && clientStateManager.SupportsClientState)
				{
					dictionary2["state"] = clientStateManager.SaveClientState();
				}
			}
			else
			{
				dictionary2["error"] = text2;
			}
			return javaScriptSerializer.Serialize(dictionary2);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003B90 File Offset: 0x00001D90
		private static IEnumerable<ScriptReference> ScriptReferencesFromResourceEntries(IList<ScriptObjectBuilder.ResourceEntry> entries)
		{
			IList<ScriptReference> list = new List<ScriptReference>(entries.Count);
			foreach (ScriptObjectBuilder.ResourceEntry resourceEntry in entries)
			{
				list.Add(resourceEntry.ToScriptReference());
			}
			ToolkitScriptManager.ExpandAndSort(list);
			return list;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003C2C File Offset: 0x00001E2C
		private static List<ScriptObjectBuilder.ResourceEntry> GetScriptReferencesInternal(Type type, Stack<Type> typeReferenceStack)
		{
			if (typeReferenceStack.Contains(type))
			{
				throw new InvalidOperationException("Circular reference detected.");
			}
			List<ScriptObjectBuilder.ResourceEntry> list;
			if (ScriptObjectBuilder.cache.TryGetValue(type, out list))
			{
				return list;
			}
			typeReferenceStack.Push(type);
			List<ScriptObjectBuilder.ResourceEntry> result;
			try
			{
				lock (ScriptObjectBuilder.sync)
				{
					if (!ScriptObjectBuilder.cache.TryGetValue(type, out list))
					{
						list = new List<ScriptObjectBuilder.ResourceEntry>();
						List<RequiredScriptAttribute> list2 = new List<RequiredScriptAttribute>();
						foreach (RequiredScriptAttribute item in type.GetCustomAttributes(typeof(RequiredScriptAttribute), true))
						{
							list2.Add(item);
						}
						list2.Sort((RequiredScriptAttribute left, RequiredScriptAttribute right) => left.LoadOrder.CompareTo(right.LoadOrder));
						foreach (RequiredScriptAttribute requiredScriptAttribute in list2)
						{
							if (requiredScriptAttribute.ExtenderType != null)
							{
								list.AddRange(ScriptObjectBuilder.GetScriptReferencesInternal(requiredScriptAttribute.ExtenderType, typeReferenceStack));
							}
						}
						int num = 0;
						List<ScriptObjectBuilder.ResourceEntry> list3 = new List<ScriptObjectBuilder.ResourceEntry>();
						Type type2 = type;
						while (type2 != null && type2 != typeof(object))
						{
							object[] customAttributes2 = Attribute.GetCustomAttributes(type2, typeof(ClientScriptResourceAttribute), false);
							num -= customAttributes2.Length;
							foreach (ClientScriptResourceAttribute clientScriptResourceAttribute in customAttributes2)
							{
								ScriptObjectBuilder.ResourceEntry item2 = new ScriptObjectBuilder.ResourceEntry(clientScriptResourceAttribute.ResourcePath, type2, num + clientScriptResourceAttribute.LoadOrder);
								if (!list.Contains(item2) && !list3.Contains(item2))
								{
									list3.Add(item2);
								}
							}
							type2 = type2.BaseType;
						}
						list3.Sort((ScriptObjectBuilder.ResourceEntry l, ScriptObjectBuilder.ResourceEntry r) => l.Order.CompareTo(r.Order));
						list.AddRange(list3);
						ScriptObjectBuilder.cache.Add(type, list);
					}
					result = list;
				}
			}
			finally
			{
				typeReferenceStack.Pop();
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003EC8 File Offset: 0x000020C8
		private static IEnumerable<string> GetCssReferences(Control control, Type type, Stack<Type> typeReferenceStack)
		{
			if (typeReferenceStack.Contains(type))
			{
				throw new InvalidOperationException("Circular reference detected.");
			}
			IList<string> list;
			if (ScriptObjectBuilder.cssCache.TryGetValue(type, out list))
			{
				return list;
			}
			typeReferenceStack.Push(type);
			IEnumerable<string> result;
			try
			{
				lock (ScriptObjectBuilder.sync)
				{
					if (ScriptObjectBuilder.cssCache.TryGetValue(type, out list))
					{
						result = list;
					}
					else
					{
						List<string> list2 = new List<string>();
						List<RequiredScriptAttribute> list3 = new List<RequiredScriptAttribute>();
						foreach (RequiredScriptAttribute item in type.GetCustomAttributes(typeof(RequiredScriptAttribute), true))
						{
							list3.Add(item);
						}
						list3.Sort((RequiredScriptAttribute left, RequiredScriptAttribute right) => left.LoadOrder.CompareTo(right.LoadOrder));
						foreach (RequiredScriptAttribute requiredScriptAttribute in list3)
						{
							if (requiredScriptAttribute.ExtenderType != null)
							{
								list2.AddRange(ScriptObjectBuilder.GetCssReferences(control, requiredScriptAttribute.ExtenderType, typeReferenceStack));
							}
						}
						List<ScriptObjectBuilder.ResourceEntry> list4 = new List<ScriptObjectBuilder.ResourceEntry>();
						int num = 0;
						Type type2 = type;
						while (type2 != null && type2 != typeof(object))
						{
							object[] customAttributes2 = Attribute.GetCustomAttributes(type2, typeof(ClientCssResourceAttribute), false);
							num -= customAttributes2.Length;
							foreach (ClientCssResourceAttribute clientCssResourceAttribute in customAttributes2)
							{
								list4.Add(new ScriptObjectBuilder.ResourceEntry(clientCssResourceAttribute.ResourcePath, type2, num + clientCssResourceAttribute.LoadOrder));
							}
							type2 = type2.BaseType;
						}
						list4.Sort((ScriptObjectBuilder.ResourceEntry l, ScriptObjectBuilder.ResourceEntry r) => l.Order.CompareTo(r.Order));
						foreach (ScriptObjectBuilder.ResourceEntry resourceEntry in list4)
						{
							list2.Add(control.Page.ClientScript.GetWebResourceUrl(resourceEntry.ComponentType, resourceEntry.ResourcePath));
						}
						Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
						List<string> list5 = new List<string>();
						foreach (string text in list2)
						{
							if (!dictionary.ContainsKey(text))
							{
								dictionary.Add(text, null);
								list5.Add(text);
							}
						}
						list = new ReadOnlyCollection<string>(list5);
						ScriptObjectBuilder.cssCache.Add(type, list);
						result = list;
					}
				}
			}
			finally
			{
				typeReferenceStack.Pop();
			}
			return result;
		}

		// Token: 0x04000033 RID: 51
		private const string CSS_LINK = "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />";

		// Token: 0x04000034 RID: 52
		private static readonly Dictionary<Type, List<ScriptObjectBuilder.ResourceEntry>> cache = new Dictionary<Type, List<ScriptObjectBuilder.ResourceEntry>>();

		// Token: 0x04000035 RID: 53
		private static readonly Dictionary<Type, IList<string>> cssCache = new Dictionary<Type, IList<string>>();

		// Token: 0x04000036 RID: 54
		private static readonly object sync = new object();

		// Token: 0x0200001E RID: 30
		private struct ResourceEntry
		{
			// Token: 0x060000EF RID: 239 RVA: 0x00004214 File Offset: 0x00002414
			public ResourceEntry(string path, Type componentType, int order)
			{
				this.ResourcePath = path;
				this.ComponentType = componentType;
				this.Order = order;
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000422C File Offset: 0x0000242C
			private string RefKey
			{
				get
				{
					return string.Format(CultureInfo.CurrentCulture, "{0}#{1}", new object[]
					{
						(this.ComponentType == null) ? string.Empty : this.ComponentType.Assembly.FullName,
						this.ResourcePath
					});
				}
			}

			// Token: 0x060000F1 RID: 241 RVA: 0x00004283 File Offset: 0x00002483
			public static bool operator ==(ScriptObjectBuilder.ResourceEntry obj1, ScriptObjectBuilder.ResourceEntry obj2)
			{
				return obj1.Equals(obj2);
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x00004298 File Offset: 0x00002498
			public static bool operator !=(ScriptObjectBuilder.ResourceEntry obj1, ScriptObjectBuilder.ResourceEntry obj2)
			{
				return !obj1.Equals(obj2);
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x000042B0 File Offset: 0x000024B0
			public ScriptReference ToScriptReference()
			{
				return new ScriptReference
				{
					Assembly = this.ComponentType.Assembly.FullName,
					Name = this.ResourcePath
				};
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x000042E8 File Offset: 0x000024E8
			public override bool Equals(object obj)
			{
				ScriptObjectBuilder.ResourceEntry resourceEntry = (ScriptObjectBuilder.ResourceEntry)obj;
				return string.Compare(this.RefKey, resourceEntry.RefKey, true, CultureInfo.CurrentCulture) == 0;
			}

			// Token: 0x060000F5 RID: 245 RVA: 0x00004317 File Offset: 0x00002517
			public override int GetHashCode()
			{
				return this.RefKey.GetHashCode();
			}

			// Token: 0x0400003B RID: 59
			public string ResourcePath;

			// Token: 0x0400003C RID: 60
			public Type ComponentType;

			// Token: 0x0400003D RID: 61
			public int Order;
		}
	}
}
