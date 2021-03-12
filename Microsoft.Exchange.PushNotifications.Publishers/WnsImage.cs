using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000E9 RID: 233
	internal class WnsImage
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x000177DB File Offset: 0x000159DB
		public WnsImage()
		{
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000177E3 File Offset: 0x000159E3
		public WnsImage(string source, string alt = null, bool? addImageQuery = null)
		{
			this.Source = source;
			this.Alt = alt;
			this.AddImageQuery = addImageQuery;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00017800 File Offset: 0x00015A00
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x00017808 File Offset: 0x00015A08
		public string Source { get; private set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00017811 File Offset: 0x00015A11
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x00017819 File Offset: 0x00015A19
		public string Alt { get; private set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00017822 File Offset: 0x00015A22
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x0001782A File Offset: 0x00015A2A
		public bool? AddImageQuery { get; private set; }

		// Token: 0x06000780 RID: 1920 RVA: 0x00017833 File Offset: 0x00015A33
		public override string ToString()
		{
			return string.Format("{{src:{0}; alt:{1}; addImageQuery:{2}}}", this.Source.ToNullableString(), this.Alt.ToNullableString(), this.AddImageQuery.ToNullableString<bool>());
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00017860 File Offset: 0x00015A60
		internal void WriteWnsPayload(int id, WnsPayloadWriter wpw)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("id", id);
			ArgumentValidator.ThrowIfNull("wpw", wpw);
			wpw.WriteElementStart("image", false);
			wpw.WriteAttribute("id", id);
			wpw.WriteUriAttribute("src", this.Source, false);
			wpw.WriteAttribute("alt", this.Alt, true);
			wpw.WriteAttribute<bool>("addImageQuery", this.AddImageQuery, true);
			wpw.WriteElementEnd();
		}
	}
}
