using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x0200004B RID: 75
	public class MimeContentDescriptions
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x0000B810 File Offset: 0x00009A10
		public void AddMimeContentType(string contentDescription, Type type)
		{
			if (contentDescription == null)
			{
				throw new ArgumentNullException("contentDescription");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.mimeContentDescriptions.ContainsKey(contentDescription))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Content description '{0}' has already been registered", new object[]
				{
					contentDescription
				}));
			}
			if (this.mimeContentTypes.ContainsKey(type))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Content description for type'{0}' has already been registered", new object[]
				{
					type
				}));
			}
			this.mimeContentDescriptions[contentDescription] = type;
			this.mimeContentTypes[type] = contentDescription;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		public void LoadMimeContentTypes(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			Type[] types;
			try
			{
				types = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				if (ex.LoaderExceptions != null && ex.LoaderExceptions.Length > 0)
				{
					StringBuilder stringBuilder = new StringBuilder("MimeContentSerializer failed to load one or more types:" + Environment.NewLine);
					foreach (Exception ex2 in ex.LoaderExceptions)
					{
						stringBuilder.AppendLine(ex2.Message);
					}
					throw new MimeContentSerializerLoadException(stringBuilder.ToString(), ex);
				}
				throw new MimeContentSerializerLoadException(ex);
			}
			foreach (Type type in types)
			{
				string contentDescriptionInternal = MimeContentDescriptions.GetContentDescriptionInternal(type);
				if (contentDescriptionInternal != null)
				{
					this.AddMimeContentType(contentDescriptionInternal, type);
				}
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000B994 File Offset: 0x00009B94
		public string GetContentDescription<T>()
		{
			return this.GetContentDescription(typeof(T));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000B9A8 File Offset: 0x00009BA8
		public string GetContentDescription(Type type)
		{
			string result;
			if (this.mimeContentTypes.TryGetValue(type, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public Type GetTypeForContent(string contentDescription)
		{
			if (contentDescription == null)
			{
				throw new ArgumentNullException("contentDescription");
			}
			Type result;
			if (this.mimeContentDescriptions.TryGetValue(contentDescription, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		private static string GetContentDescriptionInternal(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(MimeContentAttribute), false);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length > 1)
			{
				throw new ArgumentException(string.Format("Type {0} may only have one MimeContentDescription attribute", type));
			}
			string text = ((MimeContentAttribute)customAttributes[0]).ContentDescription;
			if (text == null)
			{
				text = type.Name;
			}
			return text;
		}

		// Token: 0x04000153 RID: 339
		private readonly Dictionary<string, Type> mimeContentDescriptions = new Dictionary<string, Type>();

		// Token: 0x04000154 RID: 340
		private readonly Dictionary<Type, string> mimeContentTypes = new Dictionary<Type, string>();
	}
}
