using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000004 RID: 4
	internal class ADProvider : IADDataProvider
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000210B File Offset: 0x0000030B
		internal ADProvider(int sizeLimit, int pageSize, TimeSpan serverTimeLimit)
		{
			if (0 > pageSize || 0 > sizeLimit)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.SizeLimit = sizeLimit;
			this.PageSize = pageSize;
			this.ServerTimeLimit = serverTimeLimit;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002136 File Offset: 0x00000336
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000213E File Offset: 0x0000033E
		public int SizeLimit { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002147 File Offset: 0x00000347
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000214F File Offset: 0x0000034F
		public int PageSize { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002158 File Offset: 0x00000358
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002160 File Offset: 0x00000360
		public TimeSpan ServerTimeLimit { get; private set; }

		// Token: 0x06000010 RID: 16 RVA: 0x0000216C File Offset: 0x0000036C
		public SearchResultCollection Run(bool useGC, string directoryEntry, string[] listOfPropertiesToCollect, string filter, SearchScope searchScope)
		{
			DirectorySearcher directorySearcher = new DirectorySearcher();
			SearchResultCollection result;
			try
			{
				directorySearcher.ReferralChasing = ReferralChasingOption.All;
				directorySearcher.SearchRoot = new DirectoryEntry(directoryEntry);
				directorySearcher.Filter = filter;
				directorySearcher.PropertyNamesOnly = false;
				directorySearcher.PropertiesToLoad.AddRange(listOfPropertiesToCollect);
				directorySearcher.ServerTimeLimit = this.ServerTimeLimit;
				directorySearcher.SearchScope = searchScope;
				directorySearcher.CacheResults = useGC;
				directorySearcher.SizeLimit = this.SizeLimit;
				directorySearcher.PageSize = this.PageSize;
				result = directorySearcher.FindAll();
			}
			catch (DirectoryServicesCOMException ex)
			{
				if (ex.ExtendedError != 8333 || (!ex.ExtendedErrorMessage.StartsWith("0000208D: NameErr: DSID-0310020A, problem 2001 (NO_OBJECT), data 0, best match of:") && !ex.ExtendedErrorMessage.StartsWith("0000208D: NameErr: DSID-03100213, problem 2001 (NO_OBJECT), data 0, best match of:")))
				{
					throw;
				}
				result = null;
			}
			catch (COMException ex2)
			{
				if (!ex2.Message.StartsWith("Unknown error (0x80005000)"))
				{
					throw;
				}
				result = null;
			}
			finally
			{
				directorySearcher.Dispose();
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000226C File Offset: 0x0000046C
		public List<string> Run(bool useGC, string directoryEntry)
		{
			DirectoryEntry directoryEntry2 = null;
			List<string> list = new List<string>();
			try
			{
				directoryEntry2 = new DirectoryEntry(directoryEntry);
				list.Add(directoryEntry2.Properties["configurationNamingContext"].Value.ToString());
				list.Add(directoryEntry2.Properties["rootDomainNamingContext"].Value.ToString());
				list.Add(directoryEntry2.Properties["schemaNamingContext"].Value.ToString());
				list.Add(directoryEntry2.Properties["isGlobalCatalogReady"].Value.ToString());
			}
			catch (COMException ex)
			{
				if (ex.Message.StartsWith("Unknown error (0x80005000)"))
				{
					return null;
				}
				throw;
			}
			finally
			{
				directoryEntry2.Dispose();
			}
			return list;
		}

		// Token: 0x04000001 RID: 1
		private const int ExtendedError = 8333;

		// Token: 0x04000002 RID: 2
		private const string ExtendedErrorMessage = "0000208D: NameErr: DSID-0310020A, problem 2001 (NO_OBJECT), data 0, best match of:";

		// Token: 0x04000003 RID: 3
		private const string ExtendedErrorMessageAlt = "0000208D: NameErr: DSID-03100213, problem 2001 (NO_OBJECT), data 0, best match of:";

		// Token: 0x04000004 RID: 4
		private const string ADNotInstalledErrorMessage = "Unknown error (0x80005000)";
	}
}
