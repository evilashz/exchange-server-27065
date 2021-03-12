using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200117E RID: 4478
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringItemAlreadyHasGlobalOverrideException : LocalizedException
	{
		// Token: 0x0600B660 RID: 46688 RVA: 0x0029FB2A File Offset: 0x0029DD2A
		public MonitoringItemAlreadyHasGlobalOverrideException(string workitem, string overrideName, string workitemType) : base(Strings.MonitoringItemAlreadyHasGlobalOverride(workitem, overrideName, workitemType))
		{
			this.workitem = workitem;
			this.overrideName = overrideName;
			this.workitemType = workitemType;
		}

		// Token: 0x0600B661 RID: 46689 RVA: 0x0029FB4F File Offset: 0x0029DD4F
		public MonitoringItemAlreadyHasGlobalOverrideException(string workitem, string overrideName, string workitemType, Exception innerException) : base(Strings.MonitoringItemAlreadyHasGlobalOverride(workitem, overrideName, workitemType), innerException)
		{
			this.workitem = workitem;
			this.overrideName = overrideName;
			this.workitemType = workitemType;
		}

		// Token: 0x0600B662 RID: 46690 RVA: 0x0029FB78 File Offset: 0x0029DD78
		protected MonitoringItemAlreadyHasGlobalOverrideException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.workitem = (string)info.GetValue("workitem", typeof(string));
			this.overrideName = (string)info.GetValue("overrideName", typeof(string));
			this.workitemType = (string)info.GetValue("workitemType", typeof(string));
		}

		// Token: 0x0600B663 RID: 46691 RVA: 0x0029FBED File Offset: 0x0029DDED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("workitem", this.workitem);
			info.AddValue("overrideName", this.overrideName);
			info.AddValue("workitemType", this.workitemType);
		}

		// Token: 0x17003985 RID: 14725
		// (get) Token: 0x0600B664 RID: 46692 RVA: 0x0029FC2A File Offset: 0x0029DE2A
		public string Workitem
		{
			get
			{
				return this.workitem;
			}
		}

		// Token: 0x17003986 RID: 14726
		// (get) Token: 0x0600B665 RID: 46693 RVA: 0x0029FC32 File Offset: 0x0029DE32
		public string OverrideName
		{
			get
			{
				return this.overrideName;
			}
		}

		// Token: 0x17003987 RID: 14727
		// (get) Token: 0x0600B666 RID: 46694 RVA: 0x0029FC3A File Offset: 0x0029DE3A
		public string WorkitemType
		{
			get
			{
				return this.workitemType;
			}
		}

		// Token: 0x040062EB RID: 25323
		private readonly string workitem;

		// Token: 0x040062EC RID: 25324
		private readonly string overrideName;

		// Token: 0x040062ED RID: 25325
		private readonly string workitemType;
	}
}
