using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200006D RID: 109
	public struct MimeAddressReader
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x00018EE1 File Offset: 0x000170E1
		internal MimeAddressReader(MimeReader reader, bool topLevel)
		{
			this.reader = reader;
			this.topLevel = topLevel;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00018EF1 File Offset: 0x000170F1
		public bool IsGroup
		{
			get
			{
				this.AssertGood(true);
				return this.topLevel && this.reader.GroupInProgress;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00018F0F File Offset: 0x0001710F
		public MimeAddressReader GroupRecipientReader
		{
			get
			{
				if (!this.IsGroup)
				{
					throw new InvalidOperationException(Strings.AddressReaderIsNotPositionedOnAGroup);
				}
				return new MimeAddressReader(this.reader, false);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00018F30 File Offset: 0x00017130
		public string DisplayName
		{
			get
			{
				DecodingResults decodingResults;
				string result;
				if (!this.TryGetDisplayName(this.reader.HeaderDecodingOptions, out decodingResults, out result))
				{
					MimeCommon.ThrowDecodingFailedException(ref decodingResults);
				}
				return result;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00018F5C File Offset: 0x0001715C
		public string Email
		{
			get
			{
				this.AssertGood(true);
				return this.reader.ReadRecipientEmail(this.topLevel);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00018F76 File Offset: 0x00017176
		public bool ReadNextAddress()
		{
			this.AssertGood(false);
			return this.reader.ReadNextDescendant(this.topLevel);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00018F90 File Offset: 0x00017190
		public bool TryGetDisplayName(out string displayName)
		{
			DecodingResults decodingResults;
			return this.TryGetDisplayName(this.reader.HeaderDecodingOptions, out decodingResults, out displayName);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00018FB1 File Offset: 0x000171B1
		public bool TryGetDisplayName(DecodingOptions decodingOptions, out DecodingResults decodingResults, out string displayName)
		{
			this.AssertGood(true);
			return this.reader.TryReadDisplayName(this.topLevel, decodingOptions, out decodingResults, out displayName);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00018FD0 File Offset: 0x000171D0
		private void AssertGood(bool checkPositionedOnAddress)
		{
			if (this.reader == null)
			{
				throw new NotSupportedException(Strings.AddressReaderNotInitialized);
			}
			this.reader.AssertGoodToUse(true, true);
			if (this.reader.ReaderState != MimeReaderState.HeaderComplete || this.reader.CurrentHeaderObject == null || !(this.reader.CurrentHeaderObject is AddressHeader))
			{
				throw new NotSupportedException(Strings.ReaderIsNotPositionedOnAddressHeader);
			}
			if (!this.topLevel && !this.reader.GroupInProgress)
			{
				throw new InvalidOperationException(Strings.AddressReaderIsNotPositionedOnAddress);
			}
			if (checkPositionedOnAddress && !this.reader.IsCurrentChildValid(this.topLevel))
			{
				throw new InvalidOperationException(Strings.AddressReaderIsNotPositionedOnAddress);
			}
		}

		// Token: 0x0400032D RID: 813
		private MimeReader reader;

		// Token: 0x0400032E RID: 814
		private bool topLevel;
	}
}
