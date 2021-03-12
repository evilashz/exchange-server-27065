using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000DD4 RID: 3540
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryValueMissingOrInvalidException : LocalizedException
	{
		// Token: 0x0600A410 RID: 42000 RVA: 0x00283995 File Offset: 0x00281B95
		public RegistryValueMissingOrInvalidException(string keyPath, string valueName) : base(Strings.RegistryValueMissingOrInvalidException(keyPath, valueName))
		{
			this.keyPath = keyPath;
			this.valueName = valueName;
		}

		// Token: 0x0600A411 RID: 42001 RVA: 0x002839B2 File Offset: 0x00281BB2
		public RegistryValueMissingOrInvalidException(string keyPath, string valueName, Exception innerException) : base(Strings.RegistryValueMissingOrInvalidException(keyPath, valueName), innerException)
		{
			this.keyPath = keyPath;
			this.valueName = valueName;
		}

		// Token: 0x0600A412 RID: 42002 RVA: 0x002839D0 File Offset: 0x00281BD0
		protected RegistryValueMissingOrInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyPath = (string)info.GetValue("keyPath", typeof(string));
			this.valueName = (string)info.GetValue("valueName", typeof(string));
		}

		// Token: 0x0600A413 RID: 42003 RVA: 0x00283A25 File Offset: 0x00281C25
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyPath", this.keyPath);
			info.AddValue("valueName", this.valueName);
		}

		// Token: 0x170035DD RID: 13789
		// (get) Token: 0x0600A414 RID: 42004 RVA: 0x00283A51 File Offset: 0x00281C51
		public string KeyPath
		{
			get
			{
				return this.keyPath;
			}
		}

		// Token: 0x170035DE RID: 13790
		// (get) Token: 0x0600A415 RID: 42005 RVA: 0x00283A59 File Offset: 0x00281C59
		public string ValueName
		{
			get
			{
				return this.valueName;
			}
		}

		// Token: 0x04005F43 RID: 24387
		private readonly string keyPath;

		// Token: 0x04005F44 RID: 24388
		private readonly string valueName;
	}
}
