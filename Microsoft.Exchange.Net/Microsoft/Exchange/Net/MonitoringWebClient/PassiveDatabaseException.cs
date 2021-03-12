using System;
using System.Text;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200077C RID: 1916
	internal class PassiveDatabaseException : HttpWebResponseWrapperException
	{
		// Token: 0x060025F6 RID: 9718 RVA: 0x00050089 File Offset: 0x0004E289
		public PassiveDatabaseException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, string passiveDetectionHint) : base(message, request, response)
		{
			this.passiveDetectionHint = passiveDetectionHint;
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x0005009C File Offset: 0x0004E29C
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.GetType().FullName + Environment.NewLine);
				stringBuilder.Append(this.ExceptionHint + Environment.NewLine);
				stringBuilder.Append(base.Message);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000500F5 File Offset: 0x0004E2F5
		public override string ExceptionHint
		{
			get
			{
				return "PassiveDatabase: " + this.passiveDetectionHint;
			}
		}

		// Token: 0x040022FF RID: 8959
		private readonly string passiveDetectionHint;
	}
}
