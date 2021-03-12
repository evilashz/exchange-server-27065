using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000089 RID: 137
	internal sealed class ADVlvPagedReader<TResult> : ADGenericPagedReader<TResult> where TResult : IConfigurable, new()
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x000253B8 File Offset: 0x000235B8
		public int OffSet
		{
			get
			{
				return this.offSet;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x000253C0 File Offset: 0x000235C0
		public int TotalCount
		{
			get
			{
				return this.totalCount;
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000253C8 File Offset: 0x000235C8
		public static ADVlvPagedReader<TResult> GetADVlvPagedReader(ADObjectId addressBook, IDirectorySession session, SortBy sortBy, bool includeBookmarkObject, bool searchForward, int pageSize, int offset, string target, IEnumerable<PropertyDefinition> properties)
		{
			if (addressBook == null)
			{
				throw new ArgumentNullException("addressBook");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			List<PropertyDefinition> list = null;
			if (properties != null)
			{
				list = new List<PropertyDefinition>(properties);
				if (!list.Contains(ADRecipientSchema.DisplayName))
				{
					list.Add(ADRecipientSchema.DisplayName);
				}
			}
			if (sortBy == null)
			{
				sortBy = new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending);
			}
			return new ADVlvPagedReader<TResult>(addressBook, session, null, QueryScope.SubTree, null, sortBy, includeBookmarkObject, searchForward, pageSize, offset, target, list);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00025440 File Offset: 0x00023640
		private ADVlvPagedReader(ADObjectId addressBook, IDirectorySession session, ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, bool includeBookmarkObject, bool searchForward, int pageSize, int offset, string target, IEnumerable<PropertyDefinition> properties) : base(session, rootId, scope, filter, sortBy, pageSize, properties, false)
		{
			if (addressBook != null)
			{
				filter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, addressBook),
					new ExistsFilter(ADRecipientSchema.DisplayName)
				});
				base.LdapFilter = LdapFilterBuilder.LdapFilterFromQueryFilter(filter, false, base.Session.SessionSettings.PartitionSoftLinkMode, base.Session.SessionSettings.IsTenantScoped);
				base.UseNullRoot = true;
			}
			this.searchForward = searchForward;
			this.includeBookmarkObject = includeBookmarkObject;
			this.offSet = offset;
			if (this.offSet == 2147483647)
			{
				this.offSet = 0;
			}
			this.vlvRequestControl = new VlvRequestControl(0, 0, target);
			base.DirectoryControls.Add(this.vlvRequestControl);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00025530 File Offset: 0x00023730
		protected override TResult[] GetNextPage()
		{
			TResult[] nextPage = base.GetNextPage();
			TResult[] array = nextPage;
			if (nextPage.Length > 0)
			{
				if (this.searchForward)
				{
					int num = 0;
					for (int i = 0; i < nextPage.Length; i++)
					{
						ADRawEntry adrawEntry = (ADRawEntry)((object)nextPage[i]);
						if (adrawEntry.Id.ObjectGuid == this.guidOfLastItem)
						{
							num = i;
							break;
						}
					}
					int num2 = this.includeBookmarkObject ? num : (num + 1);
					if (num2 > 0)
					{
						array = new TResult[nextPage.Length - num2];
						for (int j = num2; j < nextPage.Length; j++)
						{
							array[j - num2] = nextPage[j];
						}
					}
					if (array.Length > 0)
					{
						ADRawEntry adrawEntry2 = (ADRawEntry)((object)array[array.Length - 1]);
						this.guidOfLastItem = adrawEntry2.Id.ObjectGuid;
					}
				}
				else if (!this.includeBookmarkObject)
				{
					array = new TResult[nextPage.Length - 1];
					for (int k = 0; k < array.Length; k++)
					{
						array[k] = nextPage[k];
					}
				}
			}
			this.includeBookmarkObject = false;
			return array;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00025650 File Offset: 0x00023850
		protected override SearchResultEntryCollection GetNextResultCollection()
		{
			if (base.Session.NetworkCredential == null)
			{
				this.vlvRequestControl.ContextId = base.Cookie;
			}
			this.vlvRequestControl.Offset = this.offSet;
			if (this.searchForward)
			{
				this.vlvRequestControl.BeforeCount = 0;
				this.vlvRequestControl.AfterCount = (this.includeBookmarkObject ? (base.PageSize - 1) : base.PageSize);
			}
			else
			{
				this.vlvRequestControl.BeforeCount = (this.includeBookmarkObject ? (base.PageSize - 1) : base.PageSize);
				this.vlvRequestControl.AfterCount = 0;
			}
			DirectoryControl directoryControl = null;
			SearchResultEntryCollection searchResultEntryCollection = null;
			try
			{
				searchResultEntryCollection = base.GetNextResultCollection(typeof(VlvResponseControl), out directoryControl);
			}
			catch (ADInvalidHandleCookieException ex)
			{
				if (this.vlvRequestControl.ContextId == null || this.vlvRequestControl.ContextId.Length == 0)
				{
					throw;
				}
				ExTraceGlobals.ADFindTracer.TraceDebug<string>((long)this.GetHashCode(), "ADVlvPagedReader::GetNextResultCollection encounter an exception \"{0}\". Clear the cookie and try again.", ex.Message);
				this.vlvRequestControl.ContextId = null;
				searchResultEntryCollection = base.GetNextResultCollection(typeof(VlvResponseControl), out directoryControl);
			}
			ADProviderPerf.UpdateProcessCounter(Counter.ProcessRateVlv, UpdateType.Add, 1U);
			ADProviderPerf.UpdateDCCounter(base.PreferredServerName, Counter.DCRateVlv, UpdateType.Add, 1U);
			base.Cookie = ((directoryControl == null) ? null : ((VlvResponseControl)directoryControl).ContextId);
			if (!this.searchForward)
			{
				base.RetrievedAllData = new bool?(true);
			}
			if (directoryControl == null || searchResultEntryCollection.Count == 0)
			{
				base.RetrievedAllData = new bool?(true);
			}
			else
			{
				this.totalCount = ((VlvResponseControl)directoryControl).ContentCount;
				if (this.searchForward && base.PagesReturned == 0)
				{
					this.offSet = ((VlvResponseControl)directoryControl).TargetPosition;
					if (!this.includeBookmarkObject)
					{
						this.offSet++;
					}
				}
				if (!this.searchForward)
				{
					this.offSet = ((VlvResponseControl)directoryControl).TargetPosition - searchResultEntryCollection.Count + 1;
				}
				this.firstEntry = (string)searchResultEntryCollection[0].Attributes[ADRecipientSchema.DisplayName.LdapDisplayName].GetValues(typeof(string))[0];
				this.lastEntry = (string)searchResultEntryCollection[searchResultEntryCollection.Count - 1].Attributes[ADRecipientSchema.DisplayName.LdapDisplayName].GetValues(typeof(string))[0];
				if (string.Compare(this.firstEntry, this.lastEntry, new CultureInfo(base.Lcid), CompareOptions.OrdinalIgnoreCase) == 0)
				{
					base.RetrievedAllData = new bool?(true);
				}
				if (this.searchForward)
				{
					this.vlvRequestControl.Target = Encoding.UTF8.GetBytes(this.lastEntry);
				}
				else
				{
					this.vlvRequestControl.Target = Encoding.UTF8.GetBytes(this.firstEntry);
				}
			}
			return searchResultEntryCollection;
		}

		// Token: 0x04000299 RID: 665
		private string firstEntry;

		// Token: 0x0400029A RID: 666
		private string lastEntry;

		// Token: 0x0400029B RID: 667
		private bool searchForward = true;

		// Token: 0x0400029C RID: 668
		private bool includeBookmarkObject = true;

		// Token: 0x0400029D RID: 669
		private Guid guidOfLastItem = Guid.Empty;

		// Token: 0x0400029E RID: 670
		private VlvRequestControl vlvRequestControl;

		// Token: 0x0400029F RID: 671
		private int offSet = 1;

		// Token: 0x040002A0 RID: 672
		private int totalCount;
	}
}
