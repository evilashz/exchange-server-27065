using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Security.Compliance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001D2 RID: 466
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FileSystemPhotoMap
	{
		// Token: 0x06001184 RID: 4484 RVA: 0x00049415 File Offset: 0x00047615
		public FileSystemPhotoMap(string photosRootDirectoryFullPath, ITracer upstreamTracer)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("photosRootDirectoryFullPath", photosRootDirectoryFullPath);
			ArgumentValidator.ThrowIfNull("upstreamTracer", upstreamTracer);
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			this.photosRootDirectoryFullPath = photosRootDirectoryFullPath;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0004944C File Offset: 0x0004764C
		public string Map(string smtpAddress, UserPhotoSize size)
		{
			if (string.IsNullOrEmpty(smtpAddress))
			{
				throw new ArgumentNullException("smtpAddress");
			}
			SmtpAddress smtpAddress2 = FileSystemPhotoMap.ParseAndValidate(smtpAddress);
			string text = Path.Combine(this.photosRootDirectoryFullPath, FileSystemPhotoMap.GetEscapedSmtpDomainWithHash(smtpAddress2), FileSystemPhotoMap.MapUserPhotoSizeToFileSystemResolutionDirectory(size), FileSystemPhotoMap.GetEscapedSmtpLocalWithHash(smtpAddress2)) + ".jpg";
			this.tracer.TraceDebug<string, UserPhotoSize, string>((long)this.GetHashCode(), "File system photo map: mapped ({0}, {1}) to {2}", smtpAddress, size, text);
			return text;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000494B8 File Offset: 0x000476B8
		private static string Escape(string part)
		{
			StringBuilder stringBuilder = new StringBuilder(part.Length);
			foreach (char c in part)
			{
				if (FileSystemPhotoMap.IsInvalidCharInPath(c))
				{
					stringBuilder.Append('_');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0004950D File Offset: 0x0004770D
		private static bool IsInvalidCharInPath(char c)
		{
			return FileSystemPhotoMap.InvalidCharsInPath.Contains(c);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0004951C File Offset: 0x0004771C
		private static string MapUserPhotoSizeToFileSystemResolutionDirectory(UserPhotoSize size)
		{
			switch (size)
			{
			case UserPhotoSize.HR48x48:
				return "HR48x48";
			case UserPhotoSize.HR64x64:
				return "HR64x64";
			case UserPhotoSize.HR96x96:
				return "HR96x96";
			case UserPhotoSize.HR120x120:
				return "HR120x120";
			case UserPhotoSize.HR240x240:
				return "HR240x240";
			case UserPhotoSize.HR360x360:
				return "HR360x360";
			case UserPhotoSize.HR432x432:
				return "HR432x432";
			case UserPhotoSize.HR504x504:
				return "HR504x504";
			case UserPhotoSize.HR648x648:
				return "HR648x648";
			default:
				return size.ToString();
			}
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00049598 File Offset: 0x00047798
		private static SmtpAddress ParseAndValidate(string smtpAddress)
		{
			SmtpAddress smtpAddress2 = new SmtpAddress(smtpAddress);
			if (!smtpAddress2.IsValidAddress || SmtpAddress.NullReversePath.Equals(smtpAddress2))
			{
				throw new CannotMapInvalidSmtpAddressToPhotoFileException(smtpAddress);
			}
			return smtpAddress2;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000495D0 File Offset: 0x000477D0
		private static string NormalizeAndHash(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			string s2 = s.ToLowerInvariant();
			string result;
			using (MessageDigestForNonCryptographicPurposes messageDigestForNonCryptographicPurposes = new MessageDigestForNonCryptographicPurposes())
			{
				byte[] bytes = messageDigestForNonCryptographicPurposes.ComputeHash(Encoding.UTF8.GetBytes(s2));
				result = FileSystemPhotoMap.ConvertToHexadecimalSequence(bytes);
			}
			return result;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0004962C File Offset: 0x0004782C
		private static string ConvertToHexadecimalSequence(byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(bytes.Length);
			foreach (byte b in bytes)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", new object[]
				{
					b
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00049690 File Offset: 0x00047890
		private static string GetEscapedSmtpDomainWithHash(SmtpAddress smtpAddress)
		{
			return "_" + FileSystemPhotoMap.Escape(smtpAddress.Domain.Substring(0, Math.Min(30, smtpAddress.Domain.Length))) + "-" + FileSystemPhotoMap.NormalizeAndHash(smtpAddress.Domain);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000496E0 File Offset: 0x000478E0
		private static string GetEscapedSmtpLocalWithHash(SmtpAddress smtpAddress)
		{
			return "_" + FileSystemPhotoMap.Escape(smtpAddress.Local.Substring(0, Math.Min(20, smtpAddress.Local.Length))) + "-" + FileSystemPhotoMap.NormalizeAndHash(smtpAddress.ToString());
		}

		// Token: 0x04000948 RID: 2376
		private const string PhotoFileExtension = ".jpg";

		// Token: 0x04000949 RID: 2377
		private const int SmtpDomainPrefixLength = 30;

		// Token: 0x0400094A RID: 2378
		private const int SmtpLocalPrefixLength = 20;

		// Token: 0x0400094B RID: 2379
		private const string SmtpLocalFilePrefix = "_";

		// Token: 0x0400094C RID: 2380
		private const string SmtpDomainDirectoryPrefix = "_";

		// Token: 0x0400094D RID: 2381
		private const string SmtpLocalAndHashSeparator = "-";

		// Token: 0x0400094E RID: 2382
		private const string SmtpDomainAndHashSeparator = "-";

		// Token: 0x0400094F RID: 2383
		private static readonly HashSet<char> InvalidCharsInPath = new HashSet<char>(Path.GetInvalidFileNameChars().Union(Path.GetInvalidPathChars()));

		// Token: 0x04000950 RID: 2384
		private readonly ITracer tracer;

		// Token: 0x04000951 RID: 2385
		private readonly string photosRootDirectoryFullPath;

		// Token: 0x020001D3 RID: 467
		private static class ResolutionPartStrings
		{
			// Token: 0x04000952 RID: 2386
			internal const string HR48x48 = "HR48x48";

			// Token: 0x04000953 RID: 2387
			internal const string HR64x64 = "HR64x64";

			// Token: 0x04000954 RID: 2388
			internal const string HR96x96 = "HR96x96";

			// Token: 0x04000955 RID: 2389
			internal const string HR120x120 = "HR120x120";

			// Token: 0x04000956 RID: 2390
			internal const string HR240x240 = "HR240x240";

			// Token: 0x04000957 RID: 2391
			internal const string HR360x360 = "HR360x360";

			// Token: 0x04000958 RID: 2392
			internal const string HR432x432 = "HR432x432";

			// Token: 0x04000959 RID: 2393
			internal const string HR504x504 = "HR504x504";

			// Token: 0x0400095A RID: 2394
			internal const string HR648x648 = "HR648x648";
		}
	}
}
