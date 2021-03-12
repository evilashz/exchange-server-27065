using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B10 RID: 2832
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ProvisioningAttributeDoesNotMatchSchemaException : InvalidMailboxProvisioningAttributeException
	{
		// Token: 0x0600820E RID: 33294 RVA: 0x001A7BA4 File Offset: 0x001A5DA4
		public ProvisioningAttributeDoesNotMatchSchemaException(string keyName, string validKeys) : base(DirectoryStrings.ErrorMailboxProvisioningAttributeDoesNotMatchSchema(keyName, validKeys))
		{
			this.keyName = keyName;
			this.validKeys = validKeys;
		}

		// Token: 0x0600820F RID: 33295 RVA: 0x001A7BC1 File Offset: 0x001A5DC1
		public ProvisioningAttributeDoesNotMatchSchemaException(string keyName, string validKeys, Exception innerException) : base(DirectoryStrings.ErrorMailboxProvisioningAttributeDoesNotMatchSchema(keyName, validKeys), innerException)
		{
			this.keyName = keyName;
			this.validKeys = validKeys;
		}

		// Token: 0x06008210 RID: 33296 RVA: 0x001A7BE0 File Offset: 0x001A5DE0
		protected ProvisioningAttributeDoesNotMatchSchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyName = (string)info.GetValue("keyName", typeof(string));
			this.validKeys = (string)info.GetValue("validKeys", typeof(string));
		}

		// Token: 0x06008211 RID: 33297 RVA: 0x001A7C35 File Offset: 0x001A5E35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyName", this.keyName);
			info.AddValue("validKeys", this.validKeys);
		}

		// Token: 0x17002F25 RID: 12069
		// (get) Token: 0x06008212 RID: 33298 RVA: 0x001A7C61 File Offset: 0x001A5E61
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x17002F26 RID: 12070
		// (get) Token: 0x06008213 RID: 33299 RVA: 0x001A7C69 File Offset: 0x001A5E69
		public string ValidKeys
		{
			get
			{
				return this.validKeys;
			}
		}

		// Token: 0x040055FF RID: 22015
		private readonly string keyName;

		// Token: 0x04005600 RID: 22016
		private readonly string validKeys;
	}
}
