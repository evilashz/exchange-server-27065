using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000573 RID: 1395
	[Serializable]
	public class MapiTransactionResult
	{
		// Token: 0x0600312D RID: 12589 RVA: 0x000C86CF File Offset: 0x000C68CF
		public MapiTransactionResult()
		{
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000C86D7 File Offset: 0x000C68D7
		public MapiTransactionResult(MapiTransactionResultEnum result)
		{
			this.result = result;
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000C86E6 File Offset: 0x000C68E6
		public MapiTransactionResultEnum Value
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000C86F0 File Offset: 0x000C68F0
		public override string ToString()
		{
			string text = string.Empty;
			switch (this.result)
			{
			case MapiTransactionResultEnum.Undefined:
				text = Strings.MapiTransactionResultUndefined;
				break;
			case MapiTransactionResultEnum.Success:
				text = Strings.MapiTransactionResultSuccess;
				break;
			case MapiTransactionResultEnum.Failure:
				text = Strings.MapiTransactionResultFailure;
				break;
			case MapiTransactionResultEnum.MdbMoved:
				text = Strings.MapiTransactionResultMdbMoved;
				break;
			case MapiTransactionResultEnum.StoreNotRunning:
				text = Strings.MapiTransactionResultFailure;
				break;
			default:
				throw new MapiTransactionResultToStringCaseNotHandled(this.result);
			}
			return text;
		}

		// Token: 0x040022DA RID: 8922
		private MapiTransactionResultEnum result;
	}
}
