using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200013C RID: 316
	public class ExchangeSettingsProvider : SettingsProvider, IApplicationSettingsProvider, ISettingsProviderService
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0002C9B4 File Offset: 0x0002ABB4
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x0002C9FC File Offset: 0x0002ABFC
		public byte[] ByteData
		{
			get
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] result;
				try
				{
					BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					binaryFormatter.Serialize(memoryStream, this.settingsStore);
					result = memoryStream.ToArray();
				}
				finally
				{
					memoryStream.Close();
				}
				return result;
			}
			set
			{
				if (value != null)
				{
					MemoryStream memoryStream = new MemoryStream(value);
					try
					{
						try
						{
							BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null, new string[]
							{
								"System.Collections.Hashtable"
							});
							Hashtable hashtable = (Hashtable)binaryFormatter.Deserialize(memoryStream);
							this.settingsStore = hashtable;
						}
						catch (Exception)
						{
							this.settingsStore = new Hashtable();
						}
						return;
					}
					finally
					{
						memoryStream.Close();
					}
				}
				this.settingsStore = new Hashtable();
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002CA80 File Offset: 0x0002AC80
		public override void Initialize(string name, NameValueCollection values)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = this.ApplicationName;
			}
			base.Initialize(name, values);
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0002CA9A File Offset: 0x0002AC9A
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x0002CAA2 File Offset: 0x0002ACA2
		public override string ApplicationName
		{
			get
			{
				return this.appName;
			}
			set
			{
				this.appName = value;
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002CAAC File Offset: 0x0002ACAC
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<ExchangeSettingsProvider>(0L, "-->ExchangeSettingsProvider.GetPropertyValues: {0}", this);
			SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
			string key = (string)context["SettingsKey"];
			IDictionary dictionary = (IDictionary)this.settingsStore[key];
			foreach (object obj in properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				string name = settingsProperty.Name;
				SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
				if (dictionary == null || !dictionary.Contains(name))
				{
					goto IL_114;
				}
				ExTraceGlobals.DataFlowTracer.TraceFunction<string, object, Type>(0L, "*--ExchangeSettingsProvider.GetPropertyValues: Converting and setting value: {0} = {1} as {2}", name, dictionary[name], settingsProperty.PropertyType);
				if (dictionary[name] != null || !settingsProperty.PropertyType.IsValueType)
				{
					try
					{
						settingsPropertyValue.PropertyValue = Convert.ChangeType(dictionary[name], settingsProperty.PropertyType);
						goto IL_24D;
					}
					catch (InvalidCastException arg)
					{
						ExTraceGlobals.DataFlowTracer.TraceError<InvalidCastException>(0L, "Exception in ExchangeSettingsProvider.GetPropertyValues: {0}", arg);
						settingsPropertyValue.PropertyValue = settingsPropertyValue.Property.DefaultValue;
						goto IL_24D;
					}
					goto IL_114;
				}
				settingsPropertyValue.PropertyValue = settingsPropertyValue.Property.DefaultValue;
				IL_24D:
				settingsPropertyValueCollection.Add(settingsPropertyValue);
				continue;
				IL_114:
				if (string.IsNullOrEmpty((string)settingsProperty.DefaultValue))
				{
					ExTraceGlobals.DataFlowTracer.TraceFunction<string, Type>(0L, "*--ExchangeSettingsProvider.GetPropertyValues: Setting to null: {0} as {1}", name, settingsProperty.PropertyType);
					settingsPropertyValue.PropertyValue = null;
					goto IL_24D;
				}
				if (typeof(Enum).IsAssignableFrom(settingsProperty.PropertyType))
				{
					ExTraceGlobals.DataFlowTracer.TraceFunction<string, object, Type>(0L, "*--ExchangeSettingsProvider.GetPropertyValues: Converting and setting enum value: {0} = {1} as {2}", name, settingsProperty.DefaultValue, settingsProperty.PropertyType);
					settingsPropertyValue.PropertyValue = EnumValidator.Parse(settingsProperty.PropertyType, (string)settingsProperty.DefaultValue, EnumParseOptions.IgnoreCase);
					goto IL_24D;
				}
				MethodInfo method = settingsProperty.PropertyType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(string)
				}, null);
				if (null != method)
				{
					ExTraceGlobals.DataFlowTracer.TraceFunction<string, object, Type>(0L, "*--ExchangeSettingsProvider.GetPropertyValues: Parsing value: {0} = {1} as {2}", name, settingsProperty.DefaultValue, settingsProperty.PropertyType);
					settingsPropertyValue.PropertyValue = method.Invoke(null, new object[]
					{
						settingsProperty.DefaultValue
					});
					goto IL_24D;
				}
				ExTraceGlobals.DataFlowTracer.TraceFunction<string, object, Type>(0L, "*--ExchangeSettingsProvider.GetPropertyValues: Using default value: {0} = {1} as {2}", name, settingsProperty.DefaultValue, settingsProperty.PropertyType);
				settingsPropertyValue.SerializedValue = settingsProperty.DefaultValue;
				goto IL_24D;
			}
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<ExchangeSettingsProvider>(0L, "<--ExchangeSettingsProvider.GetPropertyValues: {0}", this);
			return settingsPropertyValueCollection;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002CD78 File Offset: 0x0002AF78
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection values)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<ExchangeSettingsProvider>(0L, "-->ExchangeSettingsProvider.SetPropertyValues: {0}", this);
			IDictionary dictionary = new Hashtable();
			foreach (object obj in values)
			{
				SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
				ExTraceGlobals.DataFlowTracer.Information<string, object, Type>(0L, "ExchangeSettingsProvider.SetPropertyValues: {0} = {1} as {2}", settingsPropertyValue.Name, settingsPropertyValue.PropertyValue, settingsPropertyValue.Property.PropertyType);
				dictionary.Add(settingsPropertyValue.Name, settingsPropertyValue.PropertyValue);
			}
			string text = (string)context["SettingsKey"];
			this.settingsStore[text] = (PSConnectionInfoSingleton.GetInstance().Enabled ? dictionary : new Hashtable());
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<ExchangeSettingsProvider, string>(0L, "<--ExchangeSettingsProvider.SetPropertyValues: {0}. settingsKey: {1}", this, text);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002CE60 File Offset: 0x0002B060
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<ExchangeSettingsProvider>(0L, "*--ExchangeSettingsProvider.GetPreviousVersion: {0}", this);
			throw new NotSupportedException();
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002CE7C File Offset: 0x0002B07C
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public void Reset(SettingsContext context)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<ExchangeSettingsProvider>(0L, "-->ExchangeSettingsProvider.Reset: {0}", this);
			string text = (string)context["SettingsKey"];
			if (this.settingsStore.ContainsKey(text))
			{
				IDictionary dictionary = (IDictionary)this.settingsStore[text];
				dictionary.Clear();
			}
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<ExchangeSettingsProvider, string>(0L, "<--ExchangeSettingsProvider.Reset: {0}. settingsKey: {1}", this, text);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002CEE5 File Offset: 0x0002B0E5
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<ExchangeSettingsProvider>(0L, "*--ExchangeSettingsProvider.Upgrade: {0}", this);
			throw new NotSupportedException();
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002CEFE File Offset: 0x0002B0FE
		public SettingsProvider GetSettingsProvider(SettingsProperty property)
		{
			if (property.Provider.GetType() == base.GetType())
			{
				return this;
			}
			return null;
		}

		// Token: 0x0400050A RID: 1290
		private string appName = "ExchangeSettingsProvider";

		// Token: 0x0400050B RID: 1291
		private Hashtable settingsStore = new Hashtable();
	}
}
