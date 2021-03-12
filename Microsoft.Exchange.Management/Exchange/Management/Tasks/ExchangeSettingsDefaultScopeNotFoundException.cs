using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001190 RID: 4496
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeSettingsDefaultScopeNotFoundException : ExchangeSettingsException
	{
		// Token: 0x0600B6C5 RID: 46789 RVA: 0x002A0731 File Offset: 0x0029E931
		public ExchangeSettingsDefaultScopeNotFoundException(string groupName) : base(Strings.ExchangeSettingsDefaultScopeNotFound(groupName))
		{
			this.groupName = groupName;
		}

		// Token: 0x0600B6C6 RID: 46790 RVA: 0x002A0746 File Offset: 0x0029E946
		public ExchangeSettingsDefaultScopeNotFoundException(string groupName, Exception innerException) : base(Strings.ExchangeSettingsDefaultScopeNotFound(groupName), innerException)
		{
			this.groupName = groupName;
		}

		// Token: 0x0600B6C7 RID: 46791 RVA: 0x002A075C File Offset: 0x0029E95C
		protected ExchangeSettingsDefaultScopeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
		}

		// Token: 0x0600B6C8 RID: 46792 RVA: 0x002A0786 File Offset: 0x0029E986
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
		}

		// Token: 0x170039A2 RID: 14754
		// (get) Token: 0x0600B6C9 RID: 46793 RVA: 0x002A07A1 File Offset: 0x0029E9A1
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x04006308 RID: 25352
		private readonly string groupName;
	}
}
