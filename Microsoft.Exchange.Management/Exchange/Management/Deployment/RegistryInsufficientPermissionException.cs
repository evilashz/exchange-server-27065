using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000DD5 RID: 3541
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryInsufficientPermissionException : LocalizedException
	{
		// Token: 0x0600A416 RID: 42006 RVA: 0x00283A61 File Offset: 0x00281C61
		public RegistryInsufficientPermissionException(string keyPath, string valueName) : base(Strings.RegistryInsufficientPermissionException(keyPath, valueName))
		{
			this.keyPath = keyPath;
			this.valueName = valueName;
		}

		// Token: 0x0600A417 RID: 42007 RVA: 0x00283A7E File Offset: 0x00281C7E
		public RegistryInsufficientPermissionException(string keyPath, string valueName, Exception innerException) : base(Strings.RegistryInsufficientPermissionException(keyPath, valueName), innerException)
		{
			this.keyPath = keyPath;
			this.valueName = valueName;
		}

		// Token: 0x0600A418 RID: 42008 RVA: 0x00283A9C File Offset: 0x00281C9C
		protected RegistryInsufficientPermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyPath = (string)info.GetValue("keyPath", typeof(string));
			this.valueName = (string)info.GetValue("valueName", typeof(string));
		}

		// Token: 0x0600A419 RID: 42009 RVA: 0x00283AF1 File Offset: 0x00281CF1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyPath", this.keyPath);
			info.AddValue("valueName", this.valueName);
		}

		// Token: 0x170035DF RID: 13791
		// (get) Token: 0x0600A41A RID: 42010 RVA: 0x00283B1D File Offset: 0x00281D1D
		public string KeyPath
		{
			get
			{
				return this.keyPath;
			}
		}

		// Token: 0x170035E0 RID: 13792
		// (get) Token: 0x0600A41B RID: 42011 RVA: 0x00283B25 File Offset: 0x00281D25
		public string ValueName
		{
			get
			{
				return this.valueName;
			}
		}

		// Token: 0x04005F45 RID: 24389
		private readonly string keyPath;

		// Token: 0x04005F46 RID: 24390
		private readonly string valueName;
	}
}
