using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000087 RID: 135
	internal class ResolvedRecipientDetail
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000218F1 File Offset: 0x0001FAF1
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000218F9 File Offset: 0x0001FAF9
		public string RoutingAddress
		{
			get
			{
				return this.routingAddress;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00021901 File Offset: 0x0001FB01
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00021909 File Offset: 0x0001FB09
		public string RoutingType
		{
			get
			{
				return this.routingType;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00021911 File Offset: 0x0001FB11
		public AddressOrigin AddressOrigin
		{
			get
			{
				return this.addressOrigin;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00021919 File Offset: 0x0001FB19
		public int RecipientFlags
		{
			get
			{
				return this.recipientFlags;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00021921 File Offset: 0x0001FB21
		public StoreObjectId StoreObjectId
		{
			get
			{
				return this.storeObjectId;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00021929 File Offset: 0x0001FB29
		public ADObjectId AdObjectId
		{
			get
			{
				return this.adObjectId;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00021931 File Offset: 0x0001FB31
		public string ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00021939 File Offset: 0x0001FB39
		public EmailAddressIndex EmailAddressIndex
		{
			get
			{
				return this.emailAddressIndex;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00021944 File Offset: 0x0001FB44
		public ResolvedRecipientDetail(RecipientAddress recipientAddress) : this(recipientAddress.SmtpAddress, recipientAddress.RoutingAddress, recipientAddress.DisplayName, recipientAddress.RoutingType, recipientAddress.AddressOrigin, recipientAddress.RecipientFlags, recipientAddress.StoreObjectId, recipientAddress.EmailAddressIndex, recipientAddress.ADObjectId)
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00021990 File Offset: 0x0001FB90
		public ResolvedRecipientDetail(RecipientInfoCacheEntry recipientInfoCacheEntry) : this(recipientInfoCacheEntry.SmtpAddress, recipientInfoCacheEntry.RoutingAddress, recipientInfoCacheEntry.DisplayName, recipientInfoCacheEntry.RoutingType, recipientInfoCacheEntry.AddressOrigin, recipientInfoCacheEntry.RecipientFlags, recipientInfoCacheEntry.ItemId, recipientInfoCacheEntry.EmailAddressIndex)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000219D4 File Offset: 0x0001FBD4
		public ResolvedRecipientDetail(string smtpAddress, string routingAddress, string displayName, string routingType, AddressOrigin addressOrigin, int recipientFlags, StoreObjectId storeObjectId, EmailAddressIndex emailAddressIndex, ADObjectId adObjectId)
		{
			this.smtpAddress = ResolvedRecipientDetail.EnsureNonNull(smtpAddress);
			this.routingAddress = ResolvedRecipientDetail.EnsureNonNull(routingAddress);
			this.displayName = ResolvedRecipientDetail.EnsureNonNull(displayName);
			this.routingType = ResolvedRecipientDetail.EnsureNonNull(routingType);
			this.addressOrigin = addressOrigin;
			this.recipientFlags = recipientFlags;
			this.storeObjectId = storeObjectId;
			this.adObjectId = adObjectId;
			this.emailAddressIndex = EmailAddressIndex.None;
			if (string.IsNullOrEmpty(displayName))
			{
				this.displayName = this.smtpAddress;
			}
			if (this.storeObjectId != null)
			{
				this.itemId = this.storeObjectId.ToBase64String();
				this.emailAddressIndex = emailAddressIndex;
				return;
			}
			if (this.adObjectId != null)
			{
				this.itemId = Convert.ToBase64String(this.adObjectId.ObjectGuid.ToByteArray());
				return;
			}
			this.itemId = string.Empty;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00021AA8 File Offset: 0x0001FCA8
		public ResolvedRecipientDetail(string smtpAddress, string routingAddress, string displayName, string routingType, AddressOrigin addressOrigin, int recipientFlags, string itemId, EmailAddressIndex emailAddressIndex)
		{
			this.smtpAddress = ResolvedRecipientDetail.EnsureNonNull(smtpAddress);
			this.routingAddress = ResolvedRecipientDetail.EnsureNonNull(routingAddress);
			this.displayName = ResolvedRecipientDetail.EnsureNonNull(displayName);
			this.routingType = ResolvedRecipientDetail.EnsureNonNull(routingType);
			this.addressOrigin = addressOrigin;
			this.recipientFlags = recipientFlags;
			this.itemId = ResolvedRecipientDetail.EnsureNonNull(itemId);
			this.emailAddressIndex = emailAddressIndex;
			if (string.IsNullOrEmpty(displayName))
			{
				this.displayName = this.smtpAddress;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00021B28 File Offset: 0x0001FD28
		public static ResolvedRecipientDetail[] ParseFromForm(HttpRequest request, string parameterName, bool isRequired)
		{
			string[] array = ResolvedRecipientDetail.SplitConcatStringsFromForm(request, parameterName, isRequired);
			if (array.Length % 8 != 0)
			{
				throw new OwaInvalidRequestException("Invalid account of resolved recipient details. Details received:" + string.Join("\n", array));
			}
			int num = array.Length / 8;
			List<ResolvedRecipientDetail> list = new List<ResolvedRecipientDetail>();
			for (int i = 0; i < num; i++)
			{
				int num2 = i * 8;
				string text = array[num2];
				string text2 = array[num2 + 1];
				string text3 = array[num2 + 2];
				string text4 = array[num2 + 3];
				string s = array[num2 + 4];
				string s2 = array[num2 + 5];
				string text5 = array[num2 + 6];
				string s3 = array[num2 + 7];
				int num3;
				if (!int.TryParse(s, out num3))
				{
					throw new OwaInvalidRequestException("The addressOrigin should be a valid integerDetails received:" + string.Join("\n", array));
				}
				AddressOrigin addressOrigin = (AddressOrigin)num3;
				int num4;
				if (!int.TryParse(s3, out num4))
				{
					throw new OwaInvalidRequestException("The emailAddressIndex should be a valid integerDetails received:" + string.Join("\n", array));
				}
				EmailAddressIndex emailAddressIndex = (EmailAddressIndex)num4;
				int num5;
				if (!int.TryParse(s2, out num5))
				{
					throw new OwaInvalidRequestException("The recipientFlags should be a valid integerDetails received:" + string.Join("\n", array));
				}
				ResolvedRecipientDetail item = new ResolvedRecipientDetail(text, text2, text3, text4, addressOrigin, num5, text5, emailAddressIndex);
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00021C60 File Offset: 0x0001FE60
		public void RenderConcatenatedDetails(bool requireJavascriptEncode, TextWriter writer)
		{
			string[] array = new string[8];
			array[0] = this.smtpAddress;
			array[1] = this.routingAddress;
			array[2] = this.displayName;
			array[3] = this.routingType;
			string[] array2 = array;
			int num = 4;
			int num2 = (int)this.addressOrigin;
			array2[num] = num2.ToString();
			array[5] = this.recipientFlags.ToString();
			array[6] = this.itemId;
			string[] array3 = array;
			int num3 = 7;
			int num4 = (int)this.emailAddressIndex;
			array3[num3] = num4.ToString();
			ResolvedRecipientDetail.ConcatAndRenderMultiStringsAsOneValue(requireJavascriptEncode, writer, array);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00021CDC File Offset: 0x0001FEDC
		public Participant ToParticipant()
		{
			if (!string.IsNullOrEmpty(this.itemId))
			{
				if (this.addressOrigin == AddressOrigin.Directory)
				{
					this.adObjectId = new ADObjectId(Convert.FromBase64String(this.itemId));
				}
				else if (this.addressOrigin == AddressOrigin.Store)
				{
					this.storeObjectId = StoreObjectId.Deserialize(this.itemId);
				}
			}
			Participant result = null;
			Utilities.CreateExchangeParticipant(out result, this.displayName, this.routingAddress, this.routingType, this.addressOrigin, this.storeObjectId, this.emailAddressIndex);
			return result;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00021D60 File Offset: 0x0001FF60
		private static string[] SplitConcatStringsFromForm(HttpRequest request, string parameterName, bool isRequired)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (string.IsNullOrEmpty(parameterName))
			{
				throw new ArgumentException("parameterName is null or empty.");
			}
			string formParameter = Utilities.GetFormParameter(request, parameterName, isRequired);
			if (formParameter == null)
			{
				return null;
			}
			string[] array = formParameter.Split(new char[]
			{
				'&'
			});
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = HttpUtility.UrlDecode(array[i]);
			}
			return array2;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00021DD8 File Offset: 0x0001FFD8
		private static void ConcatAndRenderMultiStringsAsOneValue(bool needJavascriptEncode, TextWriter writer, params string[] stringsToRender)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (stringsToRender == null)
			{
				throw new ArgumentNullException("stringsToRender");
			}
			for (int i = 0; i < stringsToRender.Length; i++)
			{
				if (i != 0)
				{
					Utilities.HtmlEncode("&", writer);
				}
				string s = Utilities.UrlEncode(stringsToRender[i]);
				if (needJavascriptEncode)
				{
					Utilities.HtmlEncode(Utilities.JavascriptEncode(s), writer);
				}
				else
				{
					Utilities.HtmlEncode(s, writer);
				}
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00021E3E File Offset: 0x0002003E
		private static string EnsureNonNull(string s)
		{
			if (s != null)
			{
				return s;
			}
			return string.Empty;
		}

		// Token: 0x040002EC RID: 748
		private string smtpAddress;

		// Token: 0x040002ED RID: 749
		private string routingAddress;

		// Token: 0x040002EE RID: 750
		private string displayName;

		// Token: 0x040002EF RID: 751
		private string routingType;

		// Token: 0x040002F0 RID: 752
		private AddressOrigin addressOrigin;

		// Token: 0x040002F1 RID: 753
		private int recipientFlags;

		// Token: 0x040002F2 RID: 754
		private StoreObjectId storeObjectId;

		// Token: 0x040002F3 RID: 755
		private EmailAddressIndex emailAddressIndex;

		// Token: 0x040002F4 RID: 756
		private ADObjectId adObjectId;

		// Token: 0x040002F5 RID: 757
		private string itemId;
	}
}
