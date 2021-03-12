using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000B0 RID: 176
	internal class DataBlobSession
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x000134E4 File Offset: 0x000116E4
		public DataBlobSession()
		{
			this.dataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.Spam);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00013500 File Offset: 0x00011700
		public void Save(DataBlobVersion blobVersion, IEnumerable<DataBlob> dataBlobCollection, bool saveToPrimaryOnly)
		{
			DataBlobSession.ValidateDataBlobVersionCommon(blobVersion, true);
			if (dataBlobCollection == null || !dataBlobCollection.Any<DataBlob>())
			{
				throw new ArgumentNullException("datablobCollection is empty");
			}
			blobVersion[DataBlobVersionSchema.IsCompleteBlobProperty] = false;
			foreach (DataBlob dataBlob in dataBlobCollection)
			{
				if (dataBlob.BlobId != blobVersion.BlobId)
				{
					throw new ArgumentException("Blob Id does not match with metadata");
				}
				if (dataBlob.ChunkId == -1)
				{
					throw new ArgumentException("Chunk Id is not specified. Chunk Id should be greater or equals 0");
				}
				if (blobVersion.IsCompleteBlob)
				{
					throw new ArgumentException("Only the last data chunk can have IsLastCheck set");
				}
				if (dataBlob.IsLastChunk)
				{
					blobVersion[DataBlobVersionSchema.IsCompleteBlobProperty] = true;
				}
				dataBlob.DataTypeId = blobVersion.DataTypeId;
				dataBlob[DataBlobCommonSchema.PrimaryOnlyProperty] = saveToPrimaryOnly;
				this.dataProvider.Save(dataBlob);
			}
			blobVersion[DataBlobCommonSchema.PrimaryOnlyProperty] = saveToPrimaryOnly;
			this.dataProvider.Save(blobVersion);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001361C File Offset: 0x0001181C
		public void Save(DataBlobVersion blobVersion, bool saveToPrimaryOnly)
		{
			DataBlobSession.ValidateDataBlobVersionCommon(blobVersion, true);
			blobVersion[DataBlobCommonSchema.PrimaryOnlyProperty] = saveToPrimaryOnly;
			this.dataProvider.Save(blobVersion);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00013644 File Offset: 0x00011844
		public DataBlobVersion GetLatestVersion(int dataTypeId, DataBlobVersionType versionType, bool majorVersionOnly, bool queryPrimaryOnly)
		{
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.DataTypeIdProperty, dataTypeId),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.BlobVersionTypeProperty, versionType),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.MajorVersionOnlyProperty, majorVersionOnly),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.LatestVersionOnlyProperty, true),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.PrimaryOnlyProperty, queryPrimaryOnly)
			});
			DataBlobVersion[] array = this.dataProvider.Find<DataBlobVersion>(filter, null, false, null).Cast<DataBlobVersion>().ToArray<DataBlobVersion>();
			if (array != null && array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000136EC File Offset: 0x000118EC
		public IEnumerable<DataBlobVersion> GetVersionsSince(DataBlobVersion blobVersion, DataBlobVersionType versionType, bool queryPrimaryOnly)
		{
			DataBlobSession.ValidateDataBlobVersionCommon(blobVersion, false);
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.DataTypeIdProperty, blobVersion.DataTypeId),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.MajorVersionProperty, blobVersion.Version.Major),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.MinorVersionProperty, blobVersion.Version.Minor),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.BuildNumberProperty, blobVersion.Version.Build),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.RevisionNumberProperty, blobVersion.Version.Revision),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.BlobVersionTypeProperty, versionType),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.SinceVersionOnlyProperty, true),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.PrimaryOnlyProperty, queryPrimaryOnly)
			});
			return this.dataProvider.Find<DataBlobVersion>(filter, null, false, null).Cast<DataBlobVersion>();
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000137F0 File Offset: 0x000119F0
		public IEnumerable<DataBlobVersion> GetVersionsFromLatestMajorVersion(int dataTypeId, DataBlobVersionType versionType, bool queryPrimaryOnly)
		{
			IEnumerable<DataBlobVersion> enumerable = Enumerable.Empty<DataBlobVersion>();
			DataBlobVersion latestVersion = this.GetLatestVersion(dataTypeId, versionType, true, queryPrimaryOnly);
			if (latestVersion != null)
			{
				enumerable = enumerable.Concat(new DataBlobVersion[]
				{
					latestVersion
				});
				QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.DataTypeIdProperty, latestVersion.DataTypeId),
					new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.MajorVersionProperty, latestVersion.Version.Major),
					new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.MinorVersionProperty, latestVersion.Version.Minor),
					new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.BuildNumberProperty, latestVersion.Version.Build),
					new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.RevisionNumberProperty, latestVersion.Version.Revision),
					new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.BlobVersionTypeProperty, versionType),
					new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.SinceVersionOnlyProperty, true),
					new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.PrimaryOnlyProperty, queryPrimaryOnly)
				});
				enumerable = enumerable.Concat(this.dataProvider.Find<DataBlobVersion>(filter, null, false, null).Cast<DataBlobVersion>());
			}
			return enumerable;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00013928 File Offset: 0x00011B28
		public IEnumerable<DataBlob> GetBlob(DataBlobVersion blobVersion, bool queryPrimaryOnly, ref string pageCookie, out bool complete, int pageSize = 10)
		{
			DataBlobSession.ValidateDataBlobVersionCommon(blobVersion, true);
			QueryFilter baseQueryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.DataTypeIdProperty, blobVersion.DataTypeId),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.MajorVersionProperty, blobVersion.Version.Major),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.MinorVersionProperty, blobVersion.Version.Minor),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.BuildNumberProperty, blobVersion.Version.Build),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.RevisionNumberProperty, blobVersion.Version.Revision),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.BlobIdProperty, blobVersion.BlobId),
				new ComparisonFilter(ComparisonOperator.Equal, DataBlobCommonSchema.PrimaryOnlyProperty, queryPrimaryOnly)
			});
			QueryFilter pagingQueryFilter = PagingHelper.GetPagingQueryFilter(baseQueryFilter, pageCookie);
			IEnumerable<DataBlob> result = this.dataProvider.FindPaged<DataBlob>(pagingQueryFilter, null, false, null, pageSize).Cast<DataBlob>().Cache<DataBlob>();
			pageCookie = PagingHelper.GetProcessedCookie(pagingQueryFilter, out complete);
			return result;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00013A38 File Offset: 0x00011C38
		public void Delete(DataBlobVersion blobVersion, bool deleteFromPrimary)
		{
			DataBlobSession.ValidateDataBlobVersionCommon(blobVersion, false);
			blobVersion[DataBlobCommonSchema.PrimaryOnlyProperty] = deleteFromPrimary;
			this.dataProvider.Delete(blobVersion);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00013A60 File Offset: 0x00011C60
		private static void ValidateDataBlobVersionCommon(DataBlobVersion blobVersion, bool deepCheck)
		{
			if (blobVersion == null)
			{
				throw new ArgumentNullException("blobVersion");
			}
			if (blobVersion.Version == null)
			{
				throw new ArgumentNullException("Version missing in blob version data");
			}
			if (deepCheck && blobVersion.BlobId == Guid.Empty)
			{
				throw new ArgumentNullException("BlobId missing in blob version data");
			}
		}

		// Token: 0x0400039E RID: 926
		private readonly IConfigDataProvider dataProvider;
	}
}
