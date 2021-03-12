using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000EE RID: 238
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsPropertyBadTypeException : ConfigurationSettingsException
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x0001B3B4 File Offset: 0x000195B4
		public ConfigurationSettingsPropertyBadTypeException(string name, string type) : base(DataStrings.ConfigurationSettingsPropertyBadType(name, type))
		{
			this.name = name;
			this.type = type;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001B3D1 File Offset: 0x000195D1
		public ConfigurationSettingsPropertyBadTypeException(string name, string type, Exception innerException) : base(DataStrings.ConfigurationSettingsPropertyBadType(name, type), innerException)
		{
			this.name = name;
			this.type = type;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001B3F0 File Offset: 0x000195F0
		protected ConfigurationSettingsPropertyBadTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001B445 File Offset: 0x00019645
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("type", this.type);
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001B471 File Offset: 0x00019671
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0001B479 File Offset: 0x00019679
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000590 RID: 1424
		private readonly string name;

		// Token: 0x04000591 RID: 1425
		private readonly string type;
	}
}
