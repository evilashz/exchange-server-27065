using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class GlobalLocatorServiceResult
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00004BA8 File Offset: 0x00002DA8
		public GlobalLocatorServiceResult()
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public GlobalLocatorServiceResult(GlobalLocatorServiceResultEnum result)
		{
			this.result = result;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004BBF File Offset: 0x00002DBF
		public GlobalLocatorServiceResultEnum Value
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public override string ToString()
		{
			string text = string.Empty;
			switch (this.result)
			{
			case GlobalLocatorServiceResultEnum.Undefined:
				text = Strings.GlobalLocatorServiceResultUndefined;
				break;
			case GlobalLocatorServiceResultEnum.Success:
				text = Strings.GlobalLocatorServiceResultSuccess;
				break;
			case GlobalLocatorServiceResultEnum.Failure:
				text = Strings.GlobalLocatorServiceResultFailure;
				break;
			}
			return text;
		}

		// Token: 0x04000081 RID: 129
		private GlobalLocatorServiceResultEnum result;
	}
}
