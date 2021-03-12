using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200117D RID: 4477
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PropertyAlreadyHasAnOverrideException : LocalizedException
	{
		// Token: 0x0600B659 RID: 46681 RVA: 0x0029FA11 File Offset: 0x0029DC11
		public PropertyAlreadyHasAnOverrideException(string property, string overrideName, string workitemType) : base(Strings.PropertyAlreadyHasAnOverride(property, overrideName, workitemType))
		{
			this.property = property;
			this.overrideName = overrideName;
			this.workitemType = workitemType;
		}

		// Token: 0x0600B65A RID: 46682 RVA: 0x0029FA36 File Offset: 0x0029DC36
		public PropertyAlreadyHasAnOverrideException(string property, string overrideName, string workitemType, Exception innerException) : base(Strings.PropertyAlreadyHasAnOverride(property, overrideName, workitemType), innerException)
		{
			this.property = property;
			this.overrideName = overrideName;
			this.workitemType = workitemType;
		}

		// Token: 0x0600B65B RID: 46683 RVA: 0x0029FA60 File Offset: 0x0029DC60
		protected PropertyAlreadyHasAnOverrideException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.property = (string)info.GetValue("property", typeof(string));
			this.overrideName = (string)info.GetValue("overrideName", typeof(string));
			this.workitemType = (string)info.GetValue("workitemType", typeof(string));
		}

		// Token: 0x0600B65C RID: 46684 RVA: 0x0029FAD5 File Offset: 0x0029DCD5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("property", this.property);
			info.AddValue("overrideName", this.overrideName);
			info.AddValue("workitemType", this.workitemType);
		}

		// Token: 0x17003982 RID: 14722
		// (get) Token: 0x0600B65D RID: 46685 RVA: 0x0029FB12 File Offset: 0x0029DD12
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17003983 RID: 14723
		// (get) Token: 0x0600B65E RID: 46686 RVA: 0x0029FB1A File Offset: 0x0029DD1A
		public string OverrideName
		{
			get
			{
				return this.overrideName;
			}
		}

		// Token: 0x17003984 RID: 14724
		// (get) Token: 0x0600B65F RID: 46687 RVA: 0x0029FB22 File Offset: 0x0029DD22
		public string WorkitemType
		{
			get
			{
				return this.workitemType;
			}
		}

		// Token: 0x040062E8 RID: 25320
		private readonly string property;

		// Token: 0x040062E9 RID: 25321
		private readonly string overrideName;

		// Token: 0x040062EA RID: 25322
		private readonly string workitemType;
	}
}
