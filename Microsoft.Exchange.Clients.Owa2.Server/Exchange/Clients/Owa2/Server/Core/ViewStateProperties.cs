using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200041F RID: 1055
	internal static class ViewStateProperties
	{
		// Token: 0x04001366 RID: 4966
		private const string ViewStatePropertyNamePrefix = "http://schemas.microsoft.com/exchange/";

		// Token: 0x04001367 RID: 4967
		public static readonly PropertyDefinition ReadingPanePosition = GuidNamePropertyDefinition.CreateCustom("ReadingPanePosition", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/preview", PropertyFlags.None);

		// Token: 0x04001368 RID: 4968
		public static readonly PropertyDefinition ReadingPanePositionMultiDay = GuidNamePropertyDefinition.CreateCustom("ReadingPanePositionMultiDay", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/previewMultiDay", PropertyFlags.None);

		// Token: 0x04001369 RID: 4969
		public static readonly PropertyDefinition ViewWidth = GuidNamePropertyDefinition.CreateCustom("ViewWidth", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/wcviewwidth", PropertyFlags.None);

		// Token: 0x0400136A RID: 4970
		public static readonly PropertyDefinition ViewHeight = GuidNamePropertyDefinition.CreateCustom("ViewHeight", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/wcviewheight", PropertyFlags.None);

		// Token: 0x0400136B RID: 4971
		public static readonly PropertyDefinition MultiLine = GuidNamePropertyDefinition.CreateCustom("MultiLine", typeof(bool), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/wcmultiline", PropertyFlags.None);

		// Token: 0x0400136C RID: 4972
		public static readonly PropertyDefinition SortColumn = GuidNamePropertyDefinition.CreateCustom("SortColumn", typeof(string), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/wcsortcolumn", PropertyFlags.None);

		// Token: 0x0400136D RID: 4973
		public static readonly PropertyDefinition SortOrder = GuidNamePropertyDefinition.CreateCustom("SortOrder", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/wcsortorder", PropertyFlags.None);

		// Token: 0x0400136E RID: 4974
		public static readonly PropertyDefinition CalendarViewType = GuidNamePropertyDefinition.CreateCustom("CalendarViewType", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/calviewtype", PropertyFlags.None);

		// Token: 0x0400136F RID: 4975
		public static readonly PropertyDefinition DailyViewDays = GuidNamePropertyDefinition.CreateCustom("DailyViewDays", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/dailyviewdays", PropertyFlags.None);

		// Token: 0x04001370 RID: 4976
		public static readonly PropertyDefinition TreeNodeCollapseStatus = GuidNamePropertyDefinition.CreateCustom("TreeNodeCollapseStatus", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/treenodecollapsestatus", PropertyFlags.None);

		// Token: 0x04001371 RID: 4977
		public static readonly PropertyDefinition AddressBookLookupReadingPanePosition = GuidNamePropertyDefinition.CreateCustom("AddressBookLookupReadingPanePosition", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/ablupreview", PropertyFlags.None);

		// Token: 0x04001372 RID: 4978
		public static readonly PropertyDefinition AddressBookLookupMultiLine = GuidNamePropertyDefinition.CreateCustom("AddressBookLookupMultiLine", typeof(bool), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/ablumultiline", PropertyFlags.None);

		// Token: 0x04001373 RID: 4979
		public static readonly PropertyDefinition AddressBookPickerMultiLine = GuidNamePropertyDefinition.CreateCustom("AddressBookPickerMultiLine", typeof(bool), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/abpkmultiline", PropertyFlags.None);

		// Token: 0x04001374 RID: 4980
		public static readonly PropertyDefinition ViewFilter = GuidNamePropertyDefinition.CreateCustom("ViewFilter", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/vwflt", PropertyFlags.None);

		// Token: 0x04001375 RID: 4981
		public static readonly PropertyDefinition FilteredViewLabel = GuidNamePropertyDefinition.CreateCustom("FilteredViewLabel", typeof(string[]), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fldfltr", PropertyFlags.None);

		// Token: 0x04001376 RID: 4982
		public static readonly PropertyDefinition FilteredViewAccessTime = GuidNamePropertyDefinition.CreateCustom("FilteredViewAccessTime", typeof(ExDateTime), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltract", PropertyFlags.None);

		// Token: 0x04001377 RID: 4983
		public static readonly PropertyDefinition FilteredViewFlags = GuidNamePropertyDefinition.CreateCustom("FilteredViewFlags", typeof(int), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltrflg", PropertyFlags.None);

		// Token: 0x04001378 RID: 4984
		public static readonly PropertyDefinition FilteredViewFrom = GuidNamePropertyDefinition.CreateCustom("FilteredViewFrom", typeof(string), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltrfrm", PropertyFlags.None);

		// Token: 0x04001379 RID: 4985
		public static readonly PropertyDefinition FilteredViewTo = GuidNamePropertyDefinition.CreateCustom("FilteredViewTo", typeof(string), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltrto", PropertyFlags.None);

		// Token: 0x0400137A RID: 4986
		public static readonly PropertyDefinition FilterSourceFolder = GuidNamePropertyDefinition.CreateCustom("FilterSourceFolder", typeof(string), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltrsrcfldr", PropertyFlags.None);

		// Token: 0x0400137B RID: 4987
		public static readonly PropertyDefinition ExpandedGroups = GuidNamePropertyDefinition.CreateCustom("ExpandedGroups", typeof(string[]), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/expandedgroups", PropertyFlags.None);

		// Token: 0x0400137C RID: 4988
		public static readonly PropertyDefinition SignedOutOfIM = GuidNamePropertyDefinition.CreateCustom("SignedOutOfIM", typeof(bool), ViewStateProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/signedoutofim", PropertyFlags.None);

		// Token: 0x0400137D RID: 4989
		private static readonly Guid publicStringsGuid = new Guid("00020329-0000-0000-C000-000000000046");
	}
}
