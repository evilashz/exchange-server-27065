using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001194 RID: 4500
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsCannotChangeScopeFilterOnDownlevelGroupException : ExchangeSettingsException
	{
		// Token: 0x0600B6DB RID: 46811 RVA: 0x002A09B9 File Offset: 0x0029EBB9
		public ExchangeSettingsCannotChangeScopeFilterOnDownlevelGroupException(string groupName) : base(Strings.ExchangeSettingsCannotChangeScopeFilterOnDownlevelGroup(groupName))
		{
			this.groupName = groupName;
		}

		// Token: 0x0600B6DC RID: 46812 RVA: 0x002A09CE File Offset: 0x0029EBCE
		public ExchangeSettingsCannotChangeScopeFilterOnDownlevelGroupException(string groupName, Exception innerException) : base(Strings.ExchangeSettingsCannotChangeScopeFilterOnDownlevelGroup(groupName), innerException)
		{
			this.groupName = groupName;
		}

		// Token: 0x0600B6DD RID: 46813 RVA: 0x002A09E4 File Offset: 0x0029EBE4
		protected ExchangeSettingsCannotChangeScopeFilterOnDownlevelGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
		}

		// Token: 0x0600B6DE RID: 46814 RVA: 0x002A0A0E File Offset: 0x0029EC0E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
		}

		// Token: 0x170039A8 RID: 14760
		// (get) Token: 0x0600B6DF RID: 46815 RVA: 0x002A0A29 File Offset: 0x0029EC29
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x0400630E RID: 25358
		private readonly string groupName;
	}
}
