using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002BB RID: 699
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SortOrderFormatException : LocalizedException
	{
		// Token: 0x0600191B RID: 6427 RVA: 0x0005CC81 File Offset: 0x0005AE81
		public SortOrderFormatException(string input) : base(Strings.SortOrderFormatException(input))
		{
			this.input = input;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0005CC96 File Offset: 0x0005AE96
		public SortOrderFormatException(string input, Exception innerException) : base(Strings.SortOrderFormatException(input), innerException)
		{
			this.input = input;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0005CCAC File Offset: 0x0005AEAC
		protected SortOrderFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.input = (string)info.GetValue("input", typeof(string));
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0005CCD6 File Offset: 0x0005AED6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("input", this.input);
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0005CCF1 File Offset: 0x0005AEF1
		public string Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x04000994 RID: 2452
		private readonly string input;
	}
}
