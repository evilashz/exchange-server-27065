using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F26 RID: 3878
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveIdConnectivityWCFExceptionThrown : LocalizedException
	{
		// Token: 0x0600AAB3 RID: 43699 RVA: 0x0028DE25 File Offset: 0x0028C025
		public LiveIdConnectivityWCFExceptionThrown(string e) : base(Strings.messageLiveIdConnectivityWCFExceptionThrown(e))
		{
			this.e = e;
		}

		// Token: 0x0600AAB4 RID: 43700 RVA: 0x0028DE3A File Offset: 0x0028C03A
		public LiveIdConnectivityWCFExceptionThrown(string e, Exception innerException) : base(Strings.messageLiveIdConnectivityWCFExceptionThrown(e), innerException)
		{
			this.e = e;
		}

		// Token: 0x0600AAB5 RID: 43701 RVA: 0x0028DE50 File Offset: 0x0028C050
		protected LiveIdConnectivityWCFExceptionThrown(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.e = (string)info.GetValue("e", typeof(string));
		}

		// Token: 0x0600AAB6 RID: 43702 RVA: 0x0028DE7A File Offset: 0x0028C07A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("e", this.e);
		}

		// Token: 0x17003738 RID: 14136
		// (get) Token: 0x0600AAB7 RID: 43703 RVA: 0x0028DE95 File Offset: 0x0028C095
		public string E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x0400609E RID: 24734
		private readonly string e;
	}
}
