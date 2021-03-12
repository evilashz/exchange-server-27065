using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016D RID: 365
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidRecipientTypeException : MigrationPermanentException
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x0006F7EF File Offset: 0x0006D9EF
		public InvalidRecipientTypeException(string actual, string expected) : base(Strings.ErrorInvalidRecipientType(actual, expected))
		{
			this.actual = actual;
			this.expected = expected;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0006F80C File Offset: 0x0006DA0C
		public InvalidRecipientTypeException(string actual, string expected, Exception innerException) : base(Strings.ErrorInvalidRecipientType(actual, expected), innerException)
		{
			this.actual = actual;
			this.expected = expected;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0006F82C File Offset: 0x0006DA2C
		protected InvalidRecipientTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actual = (string)info.GetValue("actual", typeof(string));
			this.expected = (string)info.GetValue("expected", typeof(string));
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0006F881 File Offset: 0x0006DA81
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actual", this.actual);
			info.AddValue("expected", this.expected);
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x0006F8AD File Offset: 0x0006DAAD
		public string Actual
		{
			get
			{
				return this.actual;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0006F8B5 File Offset: 0x0006DAB5
		public string Expected
		{
			get
			{
				return this.expected;
			}
		}

		// Token: 0x04000AF9 RID: 2809
		private readonly string actual;

		// Token: 0x04000AFA RID: 2810
		private readonly string expected;
	}
}
