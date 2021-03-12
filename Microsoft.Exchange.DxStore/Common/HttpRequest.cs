using System;
using System.Runtime.Serialization;
using FUSE.Paxos;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000076 RID: 118
	[KnownType(typeof(HttpRequest.PaxosMessage))]
	[DataContract]
	[KnownType(typeof(HttpRequest.GetStatusRequest))]
	[Serializable]
	public abstract class HttpRequest
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x00010968 File Offset: 0x0000EB68
		public HttpRequest(string sender)
		{
			this.Sender = sender;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00010977 File Offset: 0x0000EB77
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x0001097F File Offset: 0x0000EB7F
		[DataMember]
		public string Sender { get; set; }

		// Token: 0x02000077 RID: 119
		[DataContract]
		[Serializable]
		public class GetStatusRequest : HttpRequest
		{
			// Token: 0x060004D0 RID: 1232 RVA: 0x00010988 File Offset: 0x0000EB88
			public GetStatusRequest(string sender) : base(sender)
			{
			}

			// Token: 0x02000078 RID: 120
			[Serializable]
			public class Reply
			{
				// Token: 0x17000160 RID: 352
				// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00010991 File Offset: 0x0000EB91
				// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00010999 File Offset: 0x0000EB99
				public InstanceStatusInfo Info { get; set; }
			}
		}

		// Token: 0x02000079 RID: 121
		[Serializable]
		public sealed class PaxosMessage : HttpRequest
		{
			// Token: 0x060004D4 RID: 1236 RVA: 0x000109AA File Offset: 0x0000EBAA
			public PaxosMessage(string sender, Message msg) : base(sender)
			{
				this.Message = msg;
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000109BA File Offset: 0x0000EBBA
			// (set) Token: 0x060004D6 RID: 1238 RVA: 0x000109C2 File Offset: 0x0000EBC2
			public Message Message { get; set; }
		}

		// Token: 0x0200007A RID: 122
		[Serializable]
		public sealed class DxStoreRequest : HttpRequest
		{
			// Token: 0x060004D7 RID: 1239 RVA: 0x000109CB File Offset: 0x0000EBCB
			public DxStoreRequest(string self, DxStoreRequestBase r) : base(self)
			{
				this.Request = r;
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000109DB File Offset: 0x0000EBDB
			// (set) Token: 0x060004D9 RID: 1241 RVA: 0x000109E3 File Offset: 0x0000EBE3
			public DxStoreRequestBase Request { get; set; }
		}
	}
}
