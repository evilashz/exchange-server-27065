using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000ED RID: 237
	internal class PolymorphicConfiguration<T> : SimpleConfiguration<T> where T : new()
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x0001C875 File Offset: 0x0001AA75
		internal PolymorphicConfiguration()
		{
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001C880 File Offset: 0x0001AA80
		protected override void AddConfiguration(Type configurationType)
		{
			base.AddConfiguration(typeof(T));
			IEnumerable<KnownTypeAttribute> customAttributes = configurationType.GetCustomAttributes<KnownTypeAttribute>();
			foreach (KnownTypeAttribute knownTypeAttribute in customAttributes)
			{
				base.AddConfiguration(knownTypeAttribute.Type);
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001C8E4 File Offset: 0x0001AAE4
		protected override T CreateObject(Dictionary<string, string> attributes)
		{
			string typeName = null;
			if (attributes.TryGetValue("__PolymorphicConfiguration_Type", out typeName))
			{
				attributes.Remove("__PolymorphicConfiguration_Type");
				return (T)((object)Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, typeName).Unwrap());
			}
			throw new OwaInvalidOperationException(string.Format("The attribute {0} not found", "__PolymorphicConfiguration_Type"));
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001C93D File Offset: 0x0001AB3D
		protected override void WriteCustomAttributes(XmlTextWriter xmlWriter, T entry)
		{
			xmlWriter.WriteAttributeString("__PolymorphicConfiguration_Type", entry.GetType().FullName);
		}

		// Token: 0x04000545 RID: 1349
		private const string TypeAttributeName = "__PolymorphicConfiguration_Type";
	}
}
