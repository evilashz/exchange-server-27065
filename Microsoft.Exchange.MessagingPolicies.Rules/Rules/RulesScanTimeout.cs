using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200002B RID: 43
	internal class RulesScanTimeout
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007FC0 File Offset: 0x000061C0
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00007FC8 File Offset: 0x000061C8
		internal IDictionary<string, uint> ScanVelocities { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007FD1 File Offset: 0x000061D1
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00007FD9 File Offset: 0x000061D9
		internal TimeSpan MinTimeout { get; set; }

		// Token: 0x0600018F RID: 399 RVA: 0x00007FE4 File Offset: 0x000061E4
		internal RulesScanTimeout(IDictionary<string, uint> scanVelocities, int minFipsTimeoutInMilliseconds)
		{
			this.ScanVelocities = scanVelocities;
			if (!this.ScanVelocities.ContainsKey(RulesScanTimeout.DefaultVelocityKey))
			{
				this.ScanVelocities[RulesScanTimeout.DefaultVelocityKey] = RulesScanTimeout.DefaultVelocityValue;
			}
			this.MinTimeout = TimeSpan.FromMilliseconds((double)minFipsTimeoutInMilliseconds);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008034 File Offset: 0x00006234
		internal TimeSpan GetTimeout(double bodyLength, List<KeyValuePair<string, double>> attachmentNamesAndLengths)
		{
			double num = 0.0;
			num += bodyLength / (this.ScanVelocities[RulesScanTimeout.DefaultVelocityKey] * 1024U);
			if (attachmentNamesAndLengths != null)
			{
				foreach (KeyValuePair<string, double> keyValuePair in attachmentNamesAndLengths)
				{
					string key = RulesScanTimeout.DefaultVelocityKey;
					if (!string.IsNullOrEmpty(keyValuePair.Key))
					{
						key = TransportUtils.GetFileExtension(keyValuePair.Key);
					}
					uint num2;
					if (!this.ScanVelocities.TryGetValue(key, out num2))
					{
						num2 = this.ScanVelocities[RulesScanTimeout.DefaultVelocityKey];
					}
					double value = keyValuePair.Value;
					num += value / (num2 * 1024U);
				}
			}
			if ((double)((int)num) <= this.MinTimeout.TotalSeconds)
			{
				return this.MinTimeout;
			}
			return TimeSpan.FromSeconds((double)((int)num));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008128 File Offset: 0x00006328
		internal TimeSpan GetTimeout(Stream bodyStream, List<KeyValuePair<string, Stream>> attachmentNamesAndStreams)
		{
			double bodyLength = 0.0;
			if (bodyStream != null)
			{
				try
				{
					bodyLength = (double)bodyStream.Length;
				}
				catch (NotSupportedException)
				{
				}
			}
			List<KeyValuePair<string, double>> list = null;
			if (attachmentNamesAndStreams != null && attachmentNamesAndStreams.Count > 0)
			{
				list = new List<KeyValuePair<string, double>>();
				foreach (KeyValuePair<string, Stream> keyValuePair in attachmentNamesAndStreams)
				{
					Stream value = keyValuePair.Value;
					if (value != null)
					{
						try
						{
							long length = value.Length;
							list.Add(new KeyValuePair<string, double>(keyValuePair.Key, (double)length));
						}
						catch (NotSupportedException)
						{
						}
					}
				}
			}
			return this.GetTimeout(bodyLength, list);
		}

		// Token: 0x04000137 RID: 311
		internal static readonly string DefaultVelocityKey = ".";

		// Token: 0x04000138 RID: 312
		internal static readonly uint DefaultVelocityValue = 30U;
	}
}
