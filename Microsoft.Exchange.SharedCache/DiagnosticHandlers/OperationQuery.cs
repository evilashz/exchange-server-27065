using System;
using System.Collections.Generic;
using Microsoft.Exchange.SharedCache.Exceptions;

namespace Microsoft.Exchange.SharedCache.DiagnosticHandlers
{
	// Token: 0x02000014 RID: 20
	public class OperationQuery
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003744 File Offset: 0x00001944
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000374C File Offset: 0x0000194C
		public Operation Operation { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003755 File Offset: 0x00001955
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000375D File Offset: 0x0000195D
		public string CacheName { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003766 File Offset: 0x00001966
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000376E File Offset: 0x0000196E
		public string Constraint { get; set; }

		// Token: 0x06000088 RID: 136 RVA: 0x00003778 File Offset: 0x00001978
		public static bool TryCreate(string ediArgument, out OperationQuery operationQuery, out CacheOperationResult errorResult)
		{
			operationQuery = new OperationQuery();
			errorResult = null;
			string[] array = ediArgument.Split(new char[]
			{
				' '
			});
			if (array.Length != 4)
			{
				errorResult = new CacheOperationResult(operationQuery, OperationResult.ParseError, "Splitting the query by spaces should result in 4 parts");
				return false;
			}
			Operation operation;
			if (!Enum.TryParse<Operation>(array[0], true, out operation))
			{
				errorResult = new CacheOperationResult(operationQuery, OperationResult.ParseError, "Unknown operation (you supplied '" + array[0] + "'");
				return false;
			}
			operationQuery.Operation = operation;
			if (!array[2].Equals("from", StringComparison.OrdinalIgnoreCase))
			{
				errorResult = new CacheOperationResult(operationQuery, OperationResult.ParseError, "Part 3 must be the word \"from\"");
				return false;
			}
			operationQuery.Constraint = array[1].Trim();
			if (string.IsNullOrEmpty(operationQuery.Constraint))
			{
				errorResult = new CacheOperationResult(operationQuery, OperationResult.ParseError, "You must supply some constraint for the query");
				return false;
			}
			if (operation == Operation.Find && operationQuery.Constraint.Length <= 4)
			{
				errorResult = new CacheOperationResult(operationQuery, OperationResult.ParseError, "Constraints with find operation must be at least 4 characters long");
				return false;
			}
			operationQuery.CacheName = array[3].Trim();
			return true;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003870 File Offset: 0x00001A70
		public List<CacheOperationResult> Execute()
		{
			List<CacheOperationResult> list = new List<CacheOperationResult>();
			ISharedCache sharedCache;
			if (SharedCacheServer.TryGetCacheByName(this.CacheName, out sharedCache))
			{
				SharedCacheBase sharedCacheBase = sharedCache as SharedCacheBase;
				if (sharedCacheBase == null)
				{
					list.Add(new CacheOperationResult(this, OperationResult.Fail, "That cache does not derive from SharedCacheBase and can't be queried."));
					return list;
				}
				try
				{
					switch (this.Operation)
					{
					case Operation.Get:
					{
						KeyValuePair<string, CacheEntry> keyAndEntry = new KeyValuePair<string, CacheEntry>(this.Constraint, sharedCacheBase.InternalGet(this.Constraint));
						list.Add(new CacheOperationResult(this, keyAndEntry));
						break;
					}
					case Operation.Find:
					{
						KeyValuePair<string, CacheEntry>[] array = sharedCacheBase.InternalSearch(this.Constraint);
						int num = (array.Length <= CacheSettings.MaxResults.Value) ? array.Length : CacheSettings.MaxResults.Value;
						for (int i = 0; i < num; i++)
						{
							list.Add(new CacheOperationResult(this, array[i]));
						}
						break;
					}
					case Operation.Delete:
					{
						KeyValuePair<string, CacheEntry> keyAndEntry = new KeyValuePair<string, CacheEntry>(this.Constraint, sharedCacheBase.InternalDelete(this.Constraint));
						list.Add(new CacheOperationResult(this, keyAndEntry));
						break;
					}
					default:
						list.Add(new CacheOperationResult(this, OperationResult.Fail, "Operation not implemented"));
						break;
					}
				}
				catch (CorruptCacheEntryException ex)
				{
					list.Add(new CacheOperationResult(this, OperationResult.CorruptEntry, ex.ToString()));
				}
			}
			return list;
		}

		// Token: 0x04000043 RID: 67
		private const int MinFindConstraintLength = 4;
	}
}
