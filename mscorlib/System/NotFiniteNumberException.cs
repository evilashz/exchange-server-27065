using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x02000114 RID: 276
	[ComVisible(true)]
	[Serializable]
	public class NotFiniteNumberException : ArithmeticException
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x000314A0 File Offset: 0x0002F6A0
		public NotFiniteNumberException() : base(Environment.GetResourceString("Arg_NotFiniteNumberException"))
		{
			this._offendingNumber = 0.0;
			base.SetErrorCode(-2146233048);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x000314CC File Offset: 0x0002F6CC
		public NotFiniteNumberException(double offendingNumber)
		{
			this._offendingNumber = offendingNumber;
			base.SetErrorCode(-2146233048);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000314E6 File Offset: 0x0002F6E6
		public NotFiniteNumberException(string message) : base(message)
		{
			this._offendingNumber = 0.0;
			base.SetErrorCode(-2146233048);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00031509 File Offset: 0x0002F709
		public NotFiniteNumberException(string message, double offendingNumber) : base(message)
		{
			this._offendingNumber = offendingNumber;
			base.SetErrorCode(-2146233048);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00031524 File Offset: 0x0002F724
		public NotFiniteNumberException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233048);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00031539 File Offset: 0x0002F739
		public NotFiniteNumberException(string message, double offendingNumber, Exception innerException) : base(message, innerException)
		{
			this._offendingNumber = offendingNumber;
			base.SetErrorCode(-2146233048);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00031555 File Offset: 0x0002F755
		protected NotFiniteNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._offendingNumber = (double)info.GetInt32("OffendingNumber");
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00031571 File Offset: 0x0002F771
		public double OffendingNumber
		{
			get
			{
				return this._offendingNumber;
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00031579 File Offset: 0x0002F779
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("OffendingNumber", this._offendingNumber, typeof(int));
		}

		// Token: 0x040005C6 RID: 1478
		private double _offendingNumber;
	}
}
