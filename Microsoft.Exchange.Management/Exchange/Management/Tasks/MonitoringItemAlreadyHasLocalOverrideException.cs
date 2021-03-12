using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200117F RID: 4479
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringItemAlreadyHasLocalOverrideException : LocalizedException
	{
		// Token: 0x0600B667 RID: 46695 RVA: 0x0029FC42 File Offset: 0x0029DE42
		public MonitoringItemAlreadyHasLocalOverrideException(string workitem, string overrideName, string workitemType) : base(Strings.MonitoringItemAlreadyHasLocalOverride(workitem, overrideName, workitemType))
		{
			this.workitem = workitem;
			this.overrideName = overrideName;
			this.workitemType = workitemType;
		}

		// Token: 0x0600B668 RID: 46696 RVA: 0x0029FC67 File Offset: 0x0029DE67
		public MonitoringItemAlreadyHasLocalOverrideException(string workitem, string overrideName, string workitemType, Exception innerException) : base(Strings.MonitoringItemAlreadyHasLocalOverride(workitem, overrideName, workitemType), innerException)
		{
			this.workitem = workitem;
			this.overrideName = overrideName;
			this.workitemType = workitemType;
		}

		// Token: 0x0600B669 RID: 46697 RVA: 0x0029FC90 File Offset: 0x0029DE90
		protected MonitoringItemAlreadyHasLocalOverrideException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.workitem = (string)info.GetValue("workitem", typeof(string));
			this.overrideName = (string)info.GetValue("overrideName", typeof(string));
			this.workitemType = (string)info.GetValue("workitemType", typeof(string));
		}

		// Token: 0x0600B66A RID: 46698 RVA: 0x0029FD05 File Offset: 0x0029DF05
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("workitem", this.workitem);
			info.AddValue("overrideName", this.overrideName);
			info.AddValue("workitemType", this.workitemType);
		}

		// Token: 0x17003988 RID: 14728
		// (get) Token: 0x0600B66B RID: 46699 RVA: 0x0029FD42 File Offset: 0x0029DF42
		public string Workitem
		{
			get
			{
				return this.workitem;
			}
		}

		// Token: 0x17003989 RID: 14729
		// (get) Token: 0x0600B66C RID: 46700 RVA: 0x0029FD4A File Offset: 0x0029DF4A
		public string OverrideName
		{
			get
			{
				return this.overrideName;
			}
		}

		// Token: 0x1700398A RID: 14730
		// (get) Token: 0x0600B66D RID: 46701 RVA: 0x0029FD52 File Offset: 0x0029DF52
		public string WorkitemType
		{
			get
			{
				return this.workitemType;
			}
		}

		// Token: 0x040062EE RID: 25326
		private readonly string workitem;

		// Token: 0x040062EF RID: 25327
		private readonly string overrideName;

		// Token: 0x040062F0 RID: 25328
		private readonly string workitemType;
	}
}
