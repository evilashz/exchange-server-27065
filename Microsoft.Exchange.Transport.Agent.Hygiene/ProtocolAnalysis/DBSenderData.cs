using System;
using System.Collections;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	internal class DBSenderData : SenderData
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000A424 File Offset: 0x00008624
		public DBSenderData(DateTime tsCreate) : base(tsCreate)
		{
			this.heloUniqCnt = new SUniqueCount();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000A438 File Offset: 0x00008638
		public SUniqueCount UniqueHelloCount
		{
			get
			{
				return this.heloUniqCnt;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000A440 File Offset: 0x00008640
		public int UniqueHelloDomainCount
		{
			get
			{
				return this.heloUniqCnt.Count();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A44D File Offset: 0x0000864D
		public void Merge(DBSenderData source)
		{
			base.Merge(source);
			this.heloUniqCnt.Merge(source.UniqueHelloCount);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000A468 File Offset: 0x00008668
		public void Merge(FactorySenderData factoryData, string reverseDns, IPAddress senderIP, AcceptedDomainCollection acceptedDomains)
		{
			base.Merge(factoryData);
			IDictionaryEnumerator helloDomainEnumerator = factoryData.HelloDomainEnumerator;
			while (helloDomainEnumerator.MoveNext())
			{
				this.HelloDomainAnalysis((string)helloDomainEnumerator.Key, (int)helloDomainEnumerator.Value, reverseDns, senderIP, acceptedDomains);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A4B0 File Offset: 0x000086B0
		private static IPAddress GetHeloIPAddress(string helloDomain)
		{
			IPAddress result = IPAddress.Any;
			try
			{
				if (helloDomain.IndexOf(':') > 0)
				{
					result = IPAddress.Parse(helloDomain);
				}
				else if (!Regex.IsMatch(helloDomain, "[a-zA-Z_]"))
				{
					result = IPAddress.Parse(helloDomain);
				}
			}
			catch (FormatException)
			{
				result = IPAddress.Any;
			}
			return result;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A508 File Offset: 0x00008708
		private void HelloDomainAnalysis(string helloDomain, int numMsgs, string reverseDns, IPAddress senderIP, AcceptedDomainCollection acceptedDomains)
		{
			if (string.IsNullOrEmpty(helloDomain))
			{
				this.Helo[1] += numMsgs;
				return;
			}
			this.heloUniqCnt.AddItem(helloDomain);
			IPAddress heloIPAddress = DBSenderData.GetHeloIPAddress(helloDomain);
			if (heloIPAddress.Equals(IPAddress.Any))
			{
				bool flag = false;
				AcceptedDomain acceptedDomain = acceptedDomains.Find(helloDomain);
				if (acceptedDomain != null)
				{
					this.Helo[4] += numMsgs;
					flag = true;
				}
				if (!string.IsNullOrEmpty(reverseDns))
				{
					if (string.Compare(reverseDns, helloDomain, StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.Helo[2] += numMsgs;
						if (flag)
						{
							this.Helo[4] -= numMsgs;
						}
					}
					else
					{
						bool flag2 = false;
						string[] array = reverseDns.Split(new char[]
						{
							'.'
						});
						string[] array2 = helloDomain.Split(new char[]
						{
							'.'
						});
						if (array.Length >= 2 && array2.Length >= 2 && string.Compare(array[array.Length - 1], array2[array2.Length - 1], StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(array[array.Length - 2], array2[array2.Length - 2], StringComparison.OrdinalIgnoreCase) == 0)
						{
							if (array2[array2.Length - 1].Length <= 2)
							{
								if (array.Length >= 3 && array2.Length >= 3 && string.Compare(array[array.Length - 3], array2[array2.Length - 3], StringComparison.OrdinalIgnoreCase) == 0)
								{
									flag2 = true;
								}
							}
							else
							{
								flag2 = true;
							}
						}
						if (flag2)
						{
							this.Helo[3] += numMsgs;
							if (flag)
							{
								this.Helo[4] -= numMsgs;
							}
						}
						else
						{
							this.Helo[5] += numMsgs;
						}
					}
					if (this.Helo[0] != 0)
					{
						if (this.heloUniqCnt.Count() == 1)
						{
							if (this.Helo[2] != 0)
							{
								this.Helo[2] += this.Helo[0];
							}
							else if (this.Helo[3] != 0)
							{
								this.Helo[3] += this.Helo[0];
							}
							else if (this.Helo[5] != 0)
							{
								this.Helo[5] += this.Helo[0];
							}
						}
						this.Helo[0] = 0;
						return;
					}
				}
				else
				{
					this.Helo[0] += numMsgs;
				}
				return;
			}
			if (heloIPAddress.Equals(senderIP))
			{
				this.Helo[2] += numMsgs;
				return;
			}
			this.Helo[5] += numMsgs;
		}

		// Token: 0x04000146 RID: 326
		private SUniqueCount heloUniqCnt;
	}
}
