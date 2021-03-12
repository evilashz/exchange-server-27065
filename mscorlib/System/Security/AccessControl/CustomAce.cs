using System;
using System.Globalization;

namespace System.Security.AccessControl
{
	// Token: 0x02000202 RID: 514
	public sealed class CustomAce : GenericAce
	{
		// Token: 0x06001E5A RID: 7770 RVA: 0x0006A0AC File Offset: 0x000682AC
		public CustomAce(AceType type, AceFlags flags, byte[] opaque) : base(type, flags)
		{
			if (type <= AceType.SystemAlarmCallbackObject)
			{
				throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_InvalidUserDefinedAceType"));
			}
			this.SetOpaque(opaque);
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0006A0D7 File Offset: 0x000682D7
		public int OpaqueLength
		{
			get
			{
				if (this._opaque == null)
				{
					return 0;
				}
				return this._opaque.Length;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0006A0EB File Offset: 0x000682EB
		public override int BinaryLength
		{
			get
			{
				return 4 + this.OpaqueLength;
			}
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0006A0F5 File Offset: 0x000682F5
		public byte[] GetOpaque()
		{
			return this._opaque;
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0006A100 File Offset: 0x00068300
		public void SetOpaque(byte[] opaque)
		{
			if (opaque != null)
			{
				if (opaque.Length > CustomAce.MaxOpaqueLength)
				{
					throw new ArgumentOutOfRangeException("opaque", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLength"), 0, CustomAce.MaxOpaqueLength));
				}
				if (opaque.Length % 4 != 0)
				{
					throw new ArgumentOutOfRangeException("opaque", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_ArrayLengthMultiple"), 4));
				}
			}
			this._opaque = opaque;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0006A17C File Offset: 0x0006837C
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			base.MarshalHeader(binaryForm, offset);
			offset += 4;
			if (this.OpaqueLength != 0)
			{
				if (this.OpaqueLength > CustomAce.MaxOpaqueLength)
				{
					throw new SystemException();
				}
				this.GetOpaque().CopyTo(binaryForm, offset);
			}
		}

		// Token: 0x04000AEB RID: 2795
		private byte[] _opaque;

		// Token: 0x04000AEC RID: 2796
		public static readonly int MaxOpaqueLength = 65531;
	}
}
