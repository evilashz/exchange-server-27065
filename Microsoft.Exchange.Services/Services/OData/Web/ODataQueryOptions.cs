using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000E05 RID: 3589
	internal class ODataQueryOptions
	{
		// Token: 0x06005CFC RID: 23804 RVA: 0x0012246A File Offset: 0x0012066A
		public ODataQueryOptions(HttpContext httpContext, ODataUriParser uriParser)
		{
			ArgumentValidator.ThrowIfNull("httpContext", httpContext);
			ArgumentValidator.ThrowIfNull("uriParser", uriParser);
			this.httpContext = httpContext;
			this.oDataUriParser = uriParser;
			this.Populate();
		}

		// Token: 0x06005CFD RID: 23805 RVA: 0x0012249C File Offset: 0x0012069C
		private ODataQueryOptions()
		{
		}

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06005CFE RID: 23806 RVA: 0x001224A4 File Offset: 0x001206A4
		// (set) Token: 0x06005CFF RID: 23807 RVA: 0x001224AC File Offset: 0x001206AC
		public int? Top { get; private set; }

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06005D00 RID: 23808 RVA: 0x001224B5 File Offset: 0x001206B5
		// (set) Token: 0x06005D01 RID: 23809 RVA: 0x001224BD File Offset: 0x001206BD
		public int? Skip { get; private set; }

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06005D02 RID: 23810 RVA: 0x001224C6 File Offset: 0x001206C6
		// (set) Token: 0x06005D03 RID: 23811 RVA: 0x001224CE File Offset: 0x001206CE
		public string[] Select { get; private set; }

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x06005D04 RID: 23812 RVA: 0x001224D7 File Offset: 0x001206D7
		// (set) Token: 0x06005D05 RID: 23813 RVA: 0x001224DF File Offset: 0x001206DF
		public FilterClause Filter { get; private set; }

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x06005D06 RID: 23814 RVA: 0x001224E8 File Offset: 0x001206E8
		// (set) Token: 0x06005D07 RID: 23815 RVA: 0x001224F0 File Offset: 0x001206F0
		public OrderByClause OrderBy { get; private set; }

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x06005D08 RID: 23816 RVA: 0x001224F9 File Offset: 0x001206F9
		// (set) Token: 0x06005D09 RID: 23817 RVA: 0x00122501 File Offset: 0x00120701
		public bool InlineCount { get; private set; }

		// Token: 0x06005D0A RID: 23818 RVA: 0x0012250A File Offset: 0x0012070A
		public bool Expands(string propertyName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("propertyName", propertyName);
			return this.rawValues != null && this.rawValues.Expand != null && this.rawValues.Expand.Contains(propertyName);
		}

		// Token: 0x06005D0B RID: 23819 RVA: 0x00122544 File Offset: 0x00120744
		private void Populate()
		{
			this.rawValues = new ODataRawQueryOptions(this.httpContext.Request.QueryString);
			if (!string.IsNullOrEmpty(this.rawValues.Top))
			{
				int value;
				if (!int.TryParse(this.rawValues.Top, out value))
				{
					throw new InvalidUrlQueryException(string.Format("$top - '{0}'", this.rawValues.Top));
				}
				this.Top = new int?(value);
			}
			if (!string.IsNullOrEmpty(this.rawValues.Skip))
			{
				int value2;
				if (!int.TryParse(this.rawValues.Skip, out value2))
				{
					throw new InvalidUrlQueryException(string.Format("$skip - '{0}'", this.rawValues.Skip));
				}
				this.Skip = new int?(value2);
			}
			if (!string.IsNullOrEmpty(this.rawValues.Select))
			{
				this.Select = this.rawValues.Select.Split(new char[]
				{
					','
				});
			}
			if (!string.IsNullOrEmpty(this.rawValues.Filter))
			{
				try
				{
					this.Filter = this.oDataUriParser.ParseFilter();
				}
				catch (ODataException internalException)
				{
					throw new InvalidUrlQueryException("$filter", internalException);
				}
			}
			if (!string.IsNullOrEmpty(this.rawValues.OrderBy))
			{
				try
				{
					this.OrderBy = this.oDataUriParser.ParseOrderBy();
				}
				catch (ODataException internalException2)
				{
					throw new InvalidUrlQueryException("$orderby", internalException2);
				}
			}
			if (!string.IsNullOrEmpty(this.rawValues.InlineCount))
			{
				string inlineCount;
				if ((inlineCount = this.rawValues.InlineCount) != null)
				{
					if (inlineCount == "allpages")
					{
						this.InlineCount = true;
						return;
					}
					if (inlineCount == "none")
					{
						this.InlineCount = false;
						return;
					}
				}
				throw new InvalidUrlQueryException(string.Format("$inlinecount - '{0}'", this.rawValues.InlineCount));
			}
		}

		// Token: 0x04003255 RID: 12885
		public static readonly ODataQueryOptions Empty = new ODataQueryOptions();

		// Token: 0x04003256 RID: 12886
		private HttpContext httpContext;

		// Token: 0x04003257 RID: 12887
		private ODataUriParser oDataUriParser;

		// Token: 0x04003258 RID: 12888
		private ODataRawQueryOptions rawValues;
	}
}
