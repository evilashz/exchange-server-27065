using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000216 RID: 534
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class DiagnosticStrideJustificationAttribute : Attribute
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00044A94 File Offset: 0x00042C94
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x00044A9C File Offset: 0x00042C9C
		public string SpoofingJustification { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00044AA5 File Offset: 0x00042CA5
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x00044AAD File Offset: 0x00042CAD
		public string TamperingJustification { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00044AB6 File Offset: 0x00042CB6
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x00044ABE File Offset: 0x00042CBE
		public string RepudiationJustification { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00044AC7 File Offset: 0x00042CC7
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x00044ACF File Offset: 0x00042CCF
		public string InformationDisclosureJustification { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00044AD8 File Offset: 0x00042CD8
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x00044AE0 File Offset: 0x00042CE0
		public string DenialOfServiceJustification { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00044AE9 File Offset: 0x00042CE9
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x00044AF1 File Offset: 0x00042CF1
		public string ElevationOfPrivligesJustification { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00044B04 File Offset: 0x00042D04
		public bool IsAuthorizedForSupportDiagnosticRoleUse
		{
			get
			{
				return !string.IsNullOrEmpty(this.SpoofingJustification) && !string.IsNullOrEmpty(this.TamperingJustification) && !string.IsNullOrEmpty(this.RepudiationJustification) && !string.IsNullOrEmpty(this.InformationDisclosureJustification) && !string.IsNullOrEmpty(this.DenialOfServiceJustification) && !string.IsNullOrEmpty(this.ElevationOfPrivligesJustification);
			}
		}
	}
}
