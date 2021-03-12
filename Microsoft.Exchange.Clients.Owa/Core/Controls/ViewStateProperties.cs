using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002D8 RID: 728
	internal sealed class ViewStateProperties
	{
		// Token: 0x06001C19 RID: 7193 RVA: 0x000A1A1D File Offset: 0x0009FC1D
		private ViewStateProperties()
		{
		}

		// Token: 0x040014C3 RID: 5315
		private const string ViewStatePropertyNamePrefix = "http://schemas.microsoft.com/exchange/";

		// Token: 0x040014C4 RID: 5316
		private static readonly Guid publicStringsGuid = new Guid("00020329-0000-0000-C000-000000000046");

		// Token: 0x040014C5 RID: 5317
		public static readonly PropertyDefinition ReadingPanePosition = GuidNamePropertyDefinition.CreateCustom("ReadingPanePosition", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/preview", PropertyFlags.None);

		// Token: 0x040014C6 RID: 5318
		public static readonly PropertyDefinition ReadingPanePositionMultiDay = GuidNamePropertyDefinition.CreateCustom("ReadingPanePositionMultiDay", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/previewMultiDay", PropertyFlags.None);

		// Token: 0x040014C7 RID: 5319
		public static readonly PropertyDefinition ViewWidth = GuidNamePropertyDefinition.CreateCustom("ViewWidth", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/wcviewwidth", PropertyFlags.None);

		// Token: 0x040014C8 RID: 5320
		public static readonly PropertyDefinition ViewHeight = GuidNamePropertyDefinition.CreateCustom("ViewHeight", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/wcviewheight", PropertyFlags.None);

		// Token: 0x040014C9 RID: 5321
		public static readonly PropertyDefinition MultiLine = GuidNamePropertyDefinition.CreateCustom("MultiLine", typeof(bool), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/wcmultiline", PropertyFlags.None);

		// Token: 0x040014CA RID: 5322
		public static readonly PropertyDefinition SortColumn = GuidNamePropertyDefinition.CreateCustom("SortColumn", typeof(string), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/wcsortcolumn", PropertyFlags.None);

		// Token: 0x040014CB RID: 5323
		public static readonly PropertyDefinition SortOrder = GuidNamePropertyDefinition.CreateCustom("SortOrder", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/wcsortorder", PropertyFlags.None);

		// Token: 0x040014CC RID: 5324
		public static readonly PropertyDefinition CalendarViewType = GuidNamePropertyDefinition.CreateCustom("CalendarViewType", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/calviewtype", PropertyFlags.None);

		// Token: 0x040014CD RID: 5325
		public static readonly PropertyDefinition DailyViewDays = GuidNamePropertyDefinition.CreateCustom("DailyViewDays", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/dailyviewdays", PropertyFlags.None);

		// Token: 0x040014CE RID: 5326
		public static readonly PropertyDefinition TreeNodeCollapseStatus = GuidNamePropertyDefinition.CreateCustom("TreeNodeCollapseStatus", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/treenodecollapsestatus", PropertyFlags.None);

		// Token: 0x040014CF RID: 5327
		public static readonly PropertyDefinition AddressBookLookupReadingPanePosition = GuidNamePropertyDefinition.CreateCustom("AddressBookLookupReadingPanePosition", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/ablupreview", PropertyFlags.None);

		// Token: 0x040014D0 RID: 5328
		public static readonly PropertyDefinition AddressBookLookupMultiLine = GuidNamePropertyDefinition.CreateCustom("AddressBookLookupMultiLine", typeof(bool), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/ablumultiline", PropertyFlags.None);

		// Token: 0x040014D1 RID: 5329
		public static readonly PropertyDefinition AddressBookPickerMultiLine = GuidNamePropertyDefinition.CreateCustom("AddressBookPickerMultiLine", typeof(bool), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/abpkmultiline", PropertyFlags.None);

		// Token: 0x040014D2 RID: 5330
		public static readonly PropertyDefinition ViewFilter = GuidNamePropertyDefinition.CreateCustom("ViewFilter", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/vwflt", PropertyFlags.None);

		// Token: 0x040014D3 RID: 5331
		public static readonly PropertyDefinition FilteredViewLabel = GuidNamePropertyDefinition.CreateCustom("FilteredViewLabel", typeof(string[]), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fldfltr", PropertyFlags.None);

		// Token: 0x040014D4 RID: 5332
		public static readonly PropertyDefinition FilteredViewAccessTime = GuidNamePropertyDefinition.CreateCustom("FilteredViewAccessTime", typeof(ExDateTime), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltract", PropertyFlags.None);

		// Token: 0x040014D5 RID: 5333
		public static readonly PropertyDefinition FilteredViewFlags = GuidNamePropertyDefinition.CreateCustom("FilteredViewFlags", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltrflg", PropertyFlags.None);

		// Token: 0x040014D6 RID: 5334
		public static readonly PropertyDefinition FilteredViewFrom = GuidNamePropertyDefinition.CreateCustom("FilteredViewFrom", typeof(string), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltrfrm", PropertyFlags.None);

		// Token: 0x040014D7 RID: 5335
		public static readonly PropertyDefinition FilteredViewTo = GuidNamePropertyDefinition.CreateCustom("FilteredViewTo", typeof(string), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltrto", PropertyFlags.None);

		// Token: 0x040014D8 RID: 5336
		public static readonly PropertyDefinition FilterSourceFolder = GuidNamePropertyDefinition.CreateCustom("FilterSourceFolder", typeof(string), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltrsrcfldr", PropertyFlags.None);

		// Token: 0x040014D9 RID: 5337
		public static readonly PropertyDefinition ExpandedGroups = GuidNamePropertyDefinition.CreateCustom("ExpandedGroups", typeof(string[]), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/expandedgroups", PropertyFlags.None);

		// Token: 0x040014DA RID: 5338
		public static readonly PropertyDefinition SignedOutOfIM = GuidNamePropertyDefinition.CreateCustom("SignedOutOfIM", typeof(bool), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/signedoutofim", PropertyFlags.None);
	}
}
