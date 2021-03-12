using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001191 RID: 4497
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsScopeAlreadyExistsException : ExchangeSettingsException
	{
		// Token: 0x0600B6CA RID: 46794 RVA: 0x002A07A9 File Offset: 0x0029E9A9
		public ExchangeSettingsScopeAlreadyExistsException(string groupName, string id) : base(Strings.ExchangeSettingsScopeAlreadyExists(groupName, id))
		{
			this.groupName = groupName;
			this.id = id;
		}

		// Token: 0x0600B6CB RID: 46795 RVA: 0x002A07C6 File Offset: 0x0029E9C6
		public ExchangeSettingsScopeAlreadyExistsException(string groupName, string id, Exception innerException) : base(Strings.ExchangeSettingsScopeAlreadyExists(groupName, id), innerException)
		{
			this.groupName = groupName;
			this.id = id;
		}

		// Token: 0x0600B6CC RID: 46796 RVA: 0x002A07E4 File Offset: 0x0029E9E4
		protected ExchangeSettingsScopeAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x0600B6CD RID: 46797 RVA: 0x002A0839 File Offset: 0x0029EA39
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
			info.AddValue("id", this.id);
		}

		// Token: 0x170039A3 RID: 14755
		// (get) Token: 0x0600B6CE RID: 46798 RVA: 0x002A0865 File Offset: 0x0029EA65
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x170039A4 RID: 14756
		// (get) Token: 0x0600B6CF RID: 46799 RVA: 0x002A086D File Offset: 0x0029EA6D
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04006309 RID: 25353
		private readonly string groupName;

		// Token: 0x0400630A RID: 25354
		private readonly string id;
	}
}
