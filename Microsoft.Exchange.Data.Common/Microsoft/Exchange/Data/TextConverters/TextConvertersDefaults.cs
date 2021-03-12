using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200016A RID: 362
	internal static class TextConvertersDefaults
	{
		// Token: 0x06000FCE RID: 4046 RVA: 0x0007565B File Offset: 0x0007385B
		public static int MinDecodeBytes(bool boundaryTest)
		{
			if (!boundaryTest)
			{
				return 64;
			}
			return 1;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00075664 File Offset: 0x00073864
		public static int InitialTokenRuns(bool boundaryTest)
		{
			if (!boundaryTest)
			{
				return 64;
			}
			return 7;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0007566D File Offset: 0x0007386D
		public static int MaxTokenRuns(bool boundaryTest)
		{
			if (!boundaryTest)
			{
				return 512;
			}
			return 16;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0007567A File Offset: 0x0007387A
		public static int InitialTokenBufferSize(bool boundaryTest)
		{
			if (!boundaryTest)
			{
				return 1024;
			}
			return 32;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00075687 File Offset: 0x00073887
		public static int MaxTokenSize(bool boundaryTest)
		{
			if (!boundaryTest)
			{
				return 4096;
			}
			return 128;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00075697 File Offset: 0x00073897
		public static int InitialHtmlAttributes(bool boundaryTest)
		{
			if (!boundaryTest)
			{
				return 8;
			}
			return 1;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0007569F File Offset: 0x0007389F
		public static int MaxHtmlAttributes(bool boundaryTest)
		{
			if (!boundaryTest)
			{
				return 128;
			}
			return 5;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000756AB File Offset: 0x000738AB
		public static int MaxHtmlNormalizerNesting(bool boundaryTest)
		{
			if (!boundaryTest)
			{
				return 4096;
			}
			return 10;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x000756B8 File Offset: 0x000738B8
		public static int MaxHtmlMetaRestartOffset(bool boundaryTest)
		{
			if (!boundaryTest)
			{
				return 4096;
			}
			return 4096;
		}

		// Token: 0x040010A0 RID: 4256
		private const int NormalMinDecodeBytes = 64;

		// Token: 0x040010A1 RID: 4257
		private const int NormalInitialTokenRuns = 64;

		// Token: 0x040010A2 RID: 4258
		private const int NormalMaxTokenRuns = 512;

		// Token: 0x040010A3 RID: 4259
		private const int NormalInitialTokenBufferSize = 1024;

		// Token: 0x040010A4 RID: 4260
		private const int NormalMaxTokenSize = 4096;

		// Token: 0x040010A5 RID: 4261
		private const int NormalInitialHtmlAttributes = 8;

		// Token: 0x040010A6 RID: 4262
		private const int NormalMaxHtmlAttributes = 128;

		// Token: 0x040010A7 RID: 4263
		private const int NormalMaxHtmlNormalizerNesting = 4096;

		// Token: 0x040010A8 RID: 4264
		private const int NormalMaxHtmlMetaRestartOffset = 4096;

		// Token: 0x040010A9 RID: 4265
		private const int BoundaryMinDecodeBytes = 1;

		// Token: 0x040010AA RID: 4266
		private const int BoundaryInitialTokenRuns = 7;

		// Token: 0x040010AB RID: 4267
		private const int BoundaryMaxTokenRuns = 16;

		// Token: 0x040010AC RID: 4268
		private const int BoundaryInitialTokenBufferSize = 32;

		// Token: 0x040010AD RID: 4269
		private const int BoundaryMaxTokenSize = 128;

		// Token: 0x040010AE RID: 4270
		private const int BoundaryInitialHtmlAttributes = 1;

		// Token: 0x040010AF RID: 4271
		private const int BoundaryMaxHtmlAttributes = 5;

		// Token: 0x040010B0 RID: 4272
		private const int BoundaryMaxHtmlNormalizerNesting = 10;

		// Token: 0x040010B1 RID: 4273
		private const int BoundaryMaxHtmlMetaRestartOffset = 4096;
	}
}
