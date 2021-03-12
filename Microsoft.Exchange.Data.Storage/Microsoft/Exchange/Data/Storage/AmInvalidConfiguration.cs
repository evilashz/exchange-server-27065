using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000D4 RID: 212
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmInvalidConfiguration : AmServerException
	{
		// Token: 0x060012A2 RID: 4770 RVA: 0x00067FDB File Offset: 0x000661DB
		public AmInvalidConfiguration(string error) : base(ServerStrings.AmInvalidConfiguration(error))
		{
			this.error = error;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00067FF5 File Offset: 0x000661F5
		public AmInvalidConfiguration(string error, Exception innerException) : base(ServerStrings.AmInvalidConfiguration(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00068010 File Offset: 0x00066210
		protected AmInvalidConfiguration(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0006803A File Offset: 0x0006623A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00068055 File Offset: 0x00066255
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000964 RID: 2404
		private readonly string error;
	}
}
