using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200118F RID: 4495
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsScopeNotFoundException : ExchangeSettingsException
	{
		// Token: 0x0600B6BF RID: 46783 RVA: 0x002A0662 File Offset: 0x0029E862
		public ExchangeSettingsScopeNotFoundException(string groupName, string id) : base(Strings.ExchangeSettingsScopeNotFound(groupName, id))
		{
			this.groupName = groupName;
			this.id = id;
		}

		// Token: 0x0600B6C0 RID: 46784 RVA: 0x002A067F File Offset: 0x0029E87F
		public ExchangeSettingsScopeNotFoundException(string groupName, string id, Exception innerException) : base(Strings.ExchangeSettingsScopeNotFound(groupName, id), innerException)
		{
			this.groupName = groupName;
			this.id = id;
		}

		// Token: 0x0600B6C1 RID: 46785 RVA: 0x002A06A0 File Offset: 0x0029E8A0
		protected ExchangeSettingsScopeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x0600B6C2 RID: 46786 RVA: 0x002A06F5 File Offset: 0x0029E8F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
			info.AddValue("id", this.id);
		}

		// Token: 0x170039A0 RID: 14752
		// (get) Token: 0x0600B6C3 RID: 46787 RVA: 0x002A0721 File Offset: 0x0029E921
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x170039A1 RID: 14753
		// (get) Token: 0x0600B6C4 RID: 46788 RVA: 0x002A0729 File Offset: 0x0029E929
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04006306 RID: 25350
		private readonly string groupName;

		// Token: 0x04006307 RID: 25351
		private readonly string id;
	}
}
