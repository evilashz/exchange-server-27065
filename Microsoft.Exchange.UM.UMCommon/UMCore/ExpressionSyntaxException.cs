using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F3 RID: 499
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ExpressionSyntaxException : LocalizedException
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x00038F09 File Offset: 0x00037109
		public ExpressionSyntaxException(string error) : base(Strings.ExpressionSyntaxException(error))
		{
			this.error = error;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00038F1E File Offset: 0x0003711E
		public ExpressionSyntaxException(string error, Exception innerException) : base(Strings.ExpressionSyntaxException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00038F34 File Offset: 0x00037134
		protected ExpressionSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00038F5E File Offset: 0x0003715E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00038F79 File Offset: 0x00037179
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000879 RID: 2169
		private readonly string error;
	}
}
