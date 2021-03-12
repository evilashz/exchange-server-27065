using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011DA RID: 4570
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CallNotAnsweredInTestUMConnectivityTask : LocalizedException
	{
		// Token: 0x0600B91E RID: 47390 RVA: 0x002A56E7 File Offset: 0x002A38E7
		public CallNotAnsweredInTestUMConnectivityTask(string timeout) : base(Strings.CallNotAnsweredInTestUMConnectivityTask(timeout))
		{
			this.timeout = timeout;
		}

		// Token: 0x0600B91F RID: 47391 RVA: 0x002A56FC File Offset: 0x002A38FC
		public CallNotAnsweredInTestUMConnectivityTask(string timeout, Exception innerException) : base(Strings.CallNotAnsweredInTestUMConnectivityTask(timeout), innerException)
		{
			this.timeout = timeout;
		}

		// Token: 0x0600B920 RID: 47392 RVA: 0x002A5712 File Offset: 0x002A3912
		protected CallNotAnsweredInTestUMConnectivityTask(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.timeout = (string)info.GetValue("timeout", typeof(string));
		}

		// Token: 0x0600B921 RID: 47393 RVA: 0x002A573C File Offset: 0x002A393C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("timeout", this.timeout);
		}

		// Token: 0x17003A33 RID: 14899
		// (get) Token: 0x0600B922 RID: 47394 RVA: 0x002A5757 File Offset: 0x002A3957
		public string Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x0400644E RID: 25678
		private readonly string timeout;
	}
}
