using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200118D RID: 4493
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsNotFoundForGroupException : ExchangeSettingsException
	{
		// Token: 0x0600B6B2 RID: 46770 RVA: 0x002A047D File Offset: 0x0029E67D
		public ExchangeSettingsNotFoundForGroupException(string groupName, string key) : base(Strings.ExchangeSettingsNotFoundForGroup(groupName, key))
		{
			this.groupName = groupName;
			this.key = key;
		}

		// Token: 0x0600B6B3 RID: 46771 RVA: 0x002A049A File Offset: 0x0029E69A
		public ExchangeSettingsNotFoundForGroupException(string groupName, string key, Exception innerException) : base(Strings.ExchangeSettingsNotFoundForGroup(groupName, key), innerException)
		{
			this.groupName = groupName;
			this.key = key;
		}

		// Token: 0x0600B6B4 RID: 46772 RVA: 0x002A04B8 File Offset: 0x0029E6B8
		protected ExchangeSettingsNotFoundForGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
			this.key = (string)info.GetValue("key", typeof(string));
		}

		// Token: 0x0600B6B5 RID: 46773 RVA: 0x002A050D File Offset: 0x0029E70D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
			info.AddValue("key", this.key);
		}

		// Token: 0x1700399B RID: 14747
		// (get) Token: 0x0600B6B6 RID: 46774 RVA: 0x002A0539 File Offset: 0x0029E739
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x1700399C RID: 14748
		// (get) Token: 0x0600B6B7 RID: 46775 RVA: 0x002A0541 File Offset: 0x0029E741
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x04006301 RID: 25345
		private readonly string groupName;

		// Token: 0x04006302 RID: 25346
		private readonly string key;
	}
}
