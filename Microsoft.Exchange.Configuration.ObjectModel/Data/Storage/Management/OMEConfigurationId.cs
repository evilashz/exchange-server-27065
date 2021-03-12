using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000155 RID: 341
	[Serializable]
	public class OMEConfigurationId : ObjectId
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00026CBE File Offset: 0x00024EBE
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x00026CB5 File Offset: 0x00024EB5
		internal byte[] Image { get; private set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00026CCF File Offset: 0x00024ECF
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x00026CC6 File Offset: 0x00024EC6
		internal string EmailText { get; private set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00026CE0 File Offset: 0x00024EE0
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x00026CD7 File Offset: 0x00024ED7
		internal string PortalText { get; private set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00026CF1 File Offset: 0x00024EF1
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x00026CE8 File Offset: 0x00024EE8
		internal string DisclaimerText { get; private set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00026D02 File Offset: 0x00024F02
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x00026CF9 File Offset: 0x00024EF9
		internal bool OTPEnabled { get; private set; }

		// Token: 0x06000C55 RID: 3157 RVA: 0x00026D0A File Offset: 0x00024F0A
		internal OMEConfigurationId(byte[] image, string emailText, string portalText, string disclaimerText, bool otpEnabled)
		{
			this.Image = image;
			this.EmailText = emailText;
			this.PortalText = portalText;
			this.DisclaimerText = disclaimerText;
			this.OTPEnabled = otpEnabled;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00026D38 File Offset: 0x00024F38
		public override byte[] GetBytes()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.EmailText);
			stringBuilder.Append(this.PortalText);
			stringBuilder.Append(this.DisclaimerText);
			stringBuilder.Append(this.OTPEnabled);
			List<byte> list = new List<byte>();
			list.AddRange(this.Image);
			list.AddRange(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
			return list.ToArray();
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00026DB0 File Offset: 0x00024FB0
		public override int GetHashCode()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.EmailText);
			stringBuilder.Append(this.PortalText);
			stringBuilder.Append(this.DisclaimerText);
			stringBuilder.Append(this.OTPEnabled);
			return stringBuilder.ToString().GetHashCode();
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00026E04 File Offset: 0x00025004
		public override bool Equals(object obj)
		{
			OMEConfigurationId omeconfigurationId = obj as OMEConfigurationId;
			return omeconfigurationId != null && (this.ByteArrayEqual(this.Image, omeconfigurationId.Image) && string.Equals(this.EmailText, omeconfigurationId.EmailText, StringComparison.Ordinal) && string.Equals(this.PortalText, omeconfigurationId.PortalText, StringComparison.Ordinal) && string.Equals(this.DisclaimerText, omeconfigurationId.DisclaimerText, StringComparison.Ordinal)) && this.OTPEnabled == omeconfigurationId.OTPEnabled;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00026E7D File Offset: 0x0002507D
		public override string ToString()
		{
			return "OME Configuration";
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00026E84 File Offset: 0x00025084
		private bool ByteArrayEqual(byte[] one, byte[] two)
		{
			byte[] first = (one == null) ? new byte[0] : one;
			byte[] second = (two == null) ? new byte[0] : two;
			return first.SequenceEqual(second);
		}

		// Token: 0x040002D5 RID: 725
		internal const string OMEConfiguration = "OME Configuration";
	}
}
