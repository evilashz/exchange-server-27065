using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F00 RID: 3840
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiTransactionResultToStringCaseNotHandled : LocalizedException
	{
		// Token: 0x0600A9E9 RID: 43497 RVA: 0x0028C815 File Offset: 0x0028AA15
		public MapiTransactionResultToStringCaseNotHandled(MapiTransactionResultEnum result) : base(Strings.MapiTransactionResultCaseNotHandled(result))
		{
			this.result = result;
		}

		// Token: 0x0600A9EA RID: 43498 RVA: 0x0028C82A File Offset: 0x0028AA2A
		public MapiTransactionResultToStringCaseNotHandled(MapiTransactionResultEnum result, Exception innerException) : base(Strings.MapiTransactionResultCaseNotHandled(result), innerException)
		{
			this.result = result;
		}

		// Token: 0x0600A9EB RID: 43499 RVA: 0x0028C840 File Offset: 0x0028AA40
		protected MapiTransactionResultToStringCaseNotHandled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.result = (MapiTransactionResultEnum)info.GetValue("result", typeof(MapiTransactionResultEnum));
		}

		// Token: 0x0600A9EC RID: 43500 RVA: 0x0028C86A File Offset: 0x0028AA6A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("result", this.result);
		}

		// Token: 0x17003706 RID: 14086
		// (get) Token: 0x0600A9ED RID: 43501 RVA: 0x0028C88A File Offset: 0x0028AA8A
		public MapiTransactionResultEnum Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x0400606C RID: 24684
		private readonly MapiTransactionResultEnum result;
	}
}
