using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200078F RID: 1935
	[Serializable]
	public class CalendarLogId : ObjectId
	{
		// Token: 0x06004419 RID: 17433 RVA: 0x00117603 File Offset: 0x00115803
		public CalendarLogId()
		{
			this.Uri = new UriHandler();
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x0011761B File Offset: 0x0011581B
		public CalendarLogId(string path)
		{
			this.Uri = new UriHandler(path);
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x00117634 File Offset: 0x00115834
		internal CalendarLogId(Item item, string user)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			byte[] property = item.GetProperty(CalendarItemBaseSchema.CleanGlobalObjectId);
			if (property != null && property.Length > 0)
			{
				this.CleanGlobalObjectId = property.To64BitString();
			}
			if (item.StoreObjectId != null)
			{
				this.StoreObjectId = item.StoreObjectId.ToBase64String();
			}
			if (item.Session != null)
			{
				this.Uri = new UriHandler(user, string.IsNullOrEmpty(this.StoreObjectId) ? this.CleanGlobalObjectId : this.StoreObjectId);
				return;
			}
			this.Uri = new UriHandler(user, string.IsNullOrEmpty(this.StoreObjectId) ? this.CleanGlobalObjectId : this.StoreObjectId);
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x0600441C RID: 17436 RVA: 0x001176F0 File Offset: 0x001158F0
		// (set) Token: 0x0600441D RID: 17437 RVA: 0x001176F8 File Offset: 0x001158F8
		public Uri Uri { get; private set; }

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x0600441E RID: 17438 RVA: 0x00117701 File Offset: 0x00115901
		public string User
		{
			get
			{
				return this.UriHandler.UserName;
			}
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x0011770E File Offset: 0x0011590E
		// (set) Token: 0x06004420 RID: 17440 RVA: 0x00117716 File Offset: 0x00115916
		public string StoreObjectId { get; private set; }

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x0011771F File Offset: 0x0011591F
		// (set) Token: 0x06004422 RID: 17442 RVA: 0x00117727 File Offset: 0x00115927
		public string CleanGlobalObjectId { get; private set; }

		// Token: 0x06004423 RID: 17443 RVA: 0x00117730 File Offset: 0x00115930
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this.Uri.ToString());
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06004424 RID: 17444 RVA: 0x00117747 File Offset: 0x00115947
		private UriHandler UriHandler
		{
			get
			{
				return new UriHandler(this.Uri);
			}
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x00117754 File Offset: 0x00115954
		public override string ToString()
		{
			return this.Uri.ToString();
		}
	}
}
