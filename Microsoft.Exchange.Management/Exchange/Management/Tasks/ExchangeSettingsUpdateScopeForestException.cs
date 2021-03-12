using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001192 RID: 4498
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsUpdateScopeForestException : ExchangeSettingsException
	{
		// Token: 0x0600B6D0 RID: 46800 RVA: 0x002A0875 File Offset: 0x0029EA75
		public ExchangeSettingsUpdateScopeForestException(string groupName, string id) : base(Strings.ExchangeSettingsUpdateScopeForest(groupName, id))
		{
			this.groupName = groupName;
			this.id = id;
		}

		// Token: 0x0600B6D1 RID: 46801 RVA: 0x002A0892 File Offset: 0x0029EA92
		public ExchangeSettingsUpdateScopeForestException(string groupName, string id, Exception innerException) : base(Strings.ExchangeSettingsUpdateScopeForest(groupName, id), innerException)
		{
			this.groupName = groupName;
			this.id = id;
		}

		// Token: 0x0600B6D2 RID: 46802 RVA: 0x002A08B0 File Offset: 0x0029EAB0
		protected ExchangeSettingsUpdateScopeForestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x0600B6D3 RID: 46803 RVA: 0x002A0905 File Offset: 0x0029EB05
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
			info.AddValue("id", this.id);
		}

		// Token: 0x170039A5 RID: 14757
		// (get) Token: 0x0600B6D4 RID: 46804 RVA: 0x002A0931 File Offset: 0x0029EB31
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x170039A6 RID: 14758
		// (get) Token: 0x0600B6D5 RID: 46805 RVA: 0x002A0939 File Offset: 0x0029EB39
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x0400630B RID: 25355
		private readonly string groupName;

		// Token: 0x0400630C RID: 25356
		private readonly string id;
	}
}
