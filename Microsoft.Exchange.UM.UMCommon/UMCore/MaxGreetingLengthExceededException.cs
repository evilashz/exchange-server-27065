using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F4 RID: 500
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MaxGreetingLengthExceededException : LocalizedException
	{
		// Token: 0x0600108C RID: 4236 RVA: 0x00038F81 File Offset: 0x00037181
		public MaxGreetingLengthExceededException(int greetingLength) : base(Strings.MaxGreetingLengthExceededException(greetingLength))
		{
			this.greetingLength = greetingLength;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00038F96 File Offset: 0x00037196
		public MaxGreetingLengthExceededException(int greetingLength, Exception innerException) : base(Strings.MaxGreetingLengthExceededException(greetingLength), innerException)
		{
			this.greetingLength = greetingLength;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00038FAC File Offset: 0x000371AC
		protected MaxGreetingLengthExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.greetingLength = (int)info.GetValue("greetingLength", typeof(int));
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00038FD6 File Offset: 0x000371D6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("greetingLength", this.greetingLength);
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00038FF1 File Offset: 0x000371F1
		public int GreetingLength
		{
			get
			{
				return this.greetingLength;
			}
		}

		// Token: 0x0400087A RID: 2170
		private readonly int greetingLength;
	}
}
