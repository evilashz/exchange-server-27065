using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011E7 RID: 4583
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SIPResouceIdConflictWithExistingValue : LocalizedException
	{
		// Token: 0x0600B962 RID: 47458 RVA: 0x002A5E07 File Offset: 0x002A4007
		public SIPResouceIdConflictWithExistingValue(string sipResId, string sipProxy) : base(Strings.ExceptionSIPResouceIdConflictWithExistingValue(sipResId, sipProxy))
		{
			this.sipResId = sipResId;
			this.sipProxy = sipProxy;
		}

		// Token: 0x0600B963 RID: 47459 RVA: 0x002A5E24 File Offset: 0x002A4024
		public SIPResouceIdConflictWithExistingValue(string sipResId, string sipProxy, Exception innerException) : base(Strings.ExceptionSIPResouceIdConflictWithExistingValue(sipResId, sipProxy), innerException)
		{
			this.sipResId = sipResId;
			this.sipProxy = sipProxy;
		}

		// Token: 0x0600B964 RID: 47460 RVA: 0x002A5E44 File Offset: 0x002A4044
		protected SIPResouceIdConflictWithExistingValue(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sipResId = (string)info.GetValue("sipResId", typeof(string));
			this.sipProxy = (string)info.GetValue("sipProxy", typeof(string));
		}

		// Token: 0x0600B965 RID: 47461 RVA: 0x002A5E99 File Offset: 0x002A4099
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sipResId", this.sipResId);
			info.AddValue("sipProxy", this.sipProxy);
		}

		// Token: 0x17003A43 RID: 14915
		// (get) Token: 0x0600B966 RID: 47462 RVA: 0x002A5EC5 File Offset: 0x002A40C5
		public string SipResId
		{
			get
			{
				return this.sipResId;
			}
		}

		// Token: 0x17003A44 RID: 14916
		// (get) Token: 0x0600B967 RID: 47463 RVA: 0x002A5ECD File Offset: 0x002A40CD
		public string SipProxy
		{
			get
			{
				return this.sipProxy;
			}
		}

		// Token: 0x0400645E RID: 25694
		private readonly string sipResId;

		// Token: 0x0400645F RID: 25695
		private readonly string sipProxy;
	}
}
