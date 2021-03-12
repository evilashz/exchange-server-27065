using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001193 RID: 4499
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsCannotChangeScopeOnScopeFilteredGroupException : ExchangeSettingsException
	{
		// Token: 0x0600B6D6 RID: 46806 RVA: 0x002A0941 File Offset: 0x0029EB41
		public ExchangeSettingsCannotChangeScopeOnScopeFilteredGroupException(string groupName) : base(Strings.ExchangeSettingsCannotChangeScopeOnScopeFilteredGroup(groupName))
		{
			this.groupName = groupName;
		}

		// Token: 0x0600B6D7 RID: 46807 RVA: 0x002A0956 File Offset: 0x0029EB56
		public ExchangeSettingsCannotChangeScopeOnScopeFilteredGroupException(string groupName, Exception innerException) : base(Strings.ExchangeSettingsCannotChangeScopeOnScopeFilteredGroup(groupName), innerException)
		{
			this.groupName = groupName;
		}

		// Token: 0x0600B6D8 RID: 46808 RVA: 0x002A096C File Offset: 0x0029EB6C
		protected ExchangeSettingsCannotChangeScopeOnScopeFilteredGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
		}

		// Token: 0x0600B6D9 RID: 46809 RVA: 0x002A0996 File Offset: 0x0029EB96
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
		}

		// Token: 0x170039A7 RID: 14759
		// (get) Token: 0x0600B6DA RID: 46810 RVA: 0x002A09B1 File Offset: 0x0029EBB1
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x0400630D RID: 25357
		private readonly string groupName;
	}
}
