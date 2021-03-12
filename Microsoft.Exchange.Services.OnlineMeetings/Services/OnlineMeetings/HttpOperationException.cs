using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	internal class HttpOperationException : OnlineMeetingSchedulerException
	{
		// Token: 0x0600020A RID: 522 RVA: 0x000079A2 File Offset: 0x00005BA2
		public HttpOperationException() : this(null)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000079AB File Offset: 0x00005BAB
		public HttpOperationException(string message) : this(message, null)
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000079B5 File Offset: 0x00005BB5
		public HttpOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000079BF File Offset: 0x00005BBF
		protected HttpOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000079C9 File Offset: 0x00005BC9
		// (set) Token: 0x0600020F RID: 527 RVA: 0x000079D1 File Offset: 0x00005BD1
		public ErrorInformation ErrorInformation { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000079DA File Offset: 0x00005BDA
		// (set) Token: 0x06000211 RID: 529 RVA: 0x000079E4 File Offset: 0x00005BE4
		public HttpWebResponse HttpResponse
		{
			get
			{
				return this.httpResponse;
			}
			set
			{
				if (this.httpResponse != value)
				{
					this.httpResponse = value;
					if (this.httpResponse == null)
					{
						this.ErrorInformation = null;
						return;
					}
					this.ErrorInformation = new ErrorInformation();
					this.ErrorInformation.Code = ErrorInformation.TryGetErrorFromHttpStatusCode(this.httpResponse.StatusCode);
					this.ErrorInformation.Message = this.httpResponse.GetReasonHeader();
				}
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007A4D File Offset: 0x00005C4D
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x04000163 RID: 355
		private HttpWebResponse httpResponse;
	}
}
