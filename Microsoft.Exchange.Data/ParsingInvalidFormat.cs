using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000E0 RID: 224
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingInvalidFormat : ParsingException
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x0001AC69 File Offset: 0x00018E69
		public ParsingInvalidFormat(string token, Type type, string invalidQuery, int position) : base(DataStrings.ExceptionInvalidFormat(token, type, invalidQuery, position))
		{
			this.token = token;
			this.type = type;
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001AC98 File Offset: 0x00018E98
		public ParsingInvalidFormat(string token, Type type, string invalidQuery, int position, Exception innerException) : base(DataStrings.ExceptionInvalidFormat(token, type, invalidQuery, position), innerException)
		{
			this.token = token;
			this.type = type;
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001ACCC File Offset: 0x00018ECC
		protected ParsingInvalidFormat(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.token = (string)info.GetValue("token", typeof(string));
			this.type = (Type)info.GetValue("type", typeof(Type));
			this.invalidQuery = (string)info.GetValue("invalidQuery", typeof(string));
			this.position = (int)info.GetValue("position", typeof(int));
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001AD64 File Offset: 0x00018F64
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("token", this.token);
			info.AddValue("type", this.type);
			info.AddValue("invalidQuery", this.invalidQuery);
			info.AddValue("position", this.position);
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0001ADBD File Offset: 0x00018FBD
		public string Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0001ADC5 File Offset: 0x00018FC5
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0001ADCD File Offset: 0x00018FCD
		public string InvalidQuery
		{
			get
			{
				return this.invalidQuery;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0001ADD5 File Offset: 0x00018FD5
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04000580 RID: 1408
		private readonly string token;

		// Token: 0x04000581 RID: 1409
		private readonly Type type;

		// Token: 0x04000582 RID: 1410
		private readonly string invalidQuery;

		// Token: 0x04000583 RID: 1411
		private readonly int position;
	}
}
