using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002A8 RID: 680
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OlcSettingNotImplementedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002303 RID: 8963 RVA: 0x0004DC26 File Offset: 0x0004BE26
		public OlcSettingNotImplementedPermanentException(string settingType, string settingName) : base(MrsStrings.OlcSettingNotImplemented(settingType, settingName))
		{
			this.settingType = settingType;
			this.settingName = settingName;
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0004DC43 File Offset: 0x0004BE43
		public OlcSettingNotImplementedPermanentException(string settingType, string settingName, Exception innerException) : base(MrsStrings.OlcSettingNotImplemented(settingType, settingName), innerException)
		{
			this.settingType = settingType;
			this.settingName = settingName;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0004DC64 File Offset: 0x0004BE64
		protected OlcSettingNotImplementedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.settingType = (string)info.GetValue("settingType", typeof(string));
			this.settingName = (string)info.GetValue("settingName", typeof(string));
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x0004DCB9 File Offset: 0x0004BEB9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("settingType", this.settingType);
			info.AddValue("settingName", this.settingName);
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x0004DCE5 File Offset: 0x0004BEE5
		public string SettingType
		{
			get
			{
				return this.settingType;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x0004DCED File Offset: 0x0004BEED
		public string SettingName
		{
			get
			{
				return this.settingName;
			}
		}

		// Token: 0x04000FAC RID: 4012
		private readonly string settingType;

		// Token: 0x04000FAD RID: 4013
		private readonly string settingName;
	}
}
