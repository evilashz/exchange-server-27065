using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001C8 RID: 456
	internal class ResultWrapper : IComparable
	{
		// Token: 0x06000D44 RID: 3396 RVA: 0x0003A97A File Offset: 0x00038B7A
		internal ResultWrapper(List<IUMRecognitionPhrase> results)
		{
			this.count = results.Count;
			this.alternateHash = ResultWrapper.GetHash(results);
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0003A99A File Offset: 0x00038B9A
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0003A9A4 File Offset: 0x00038BA4
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			ResultWrapper resultWrapper = obj as ResultWrapper;
			if (resultWrapper == null)
			{
				return -1;
			}
			int num = this.count - resultWrapper.Count;
			if (num != 0)
			{
				return num;
			}
			num = 0;
			foreach (string key in this.alternateHash.Keys)
			{
				if (!resultWrapper.Contains(key))
				{
					num = -1;
					break;
				}
			}
			return num;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003AA30 File Offset: 0x00038C30
		internal static string GetIdFromPhrase(IUMRecognitionPhrase phrase)
		{
			string result = null;
			ResultWrapper.GetResultTypeAndIdFromPhrase(phrase, out result);
			return result;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0003AA4C File Offset: 0x00038C4C
		internal static ResultType GetResultTypeAndIdFromPhrase(IUMRecognitionPhrase phrase, out string id)
		{
			id = null;
			string text = (string)phrase["ResultType"];
			ResultType result = ResultType.None;
			string a;
			if ((a = text) != null)
			{
				if (!(a == "DirectoryContact"))
				{
					if (!(a == "PersonalContact"))
					{
						if (a == "Department")
						{
							id = (string)phrase["DepartmentName"];
							result = ResultType.Department;
						}
					}
					else
					{
						id = (string)phrase["ContactId"];
						result = ResultType.PersonalContact;
					}
				}
				else
				{
					id = (string)phrase["ObjectGuid"];
					result = ResultType.DirectoryContact;
				}
			}
			return result;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003AAE0 File Offset: 0x00038CE0
		internal static Dictionary<string, IUMRecognitionPhrase> GetHash(List<IUMRecognitionPhrase> results)
		{
			Dictionary<string, IUMRecognitionPhrase> dictionary = new Dictionary<string, IUMRecognitionPhrase>();
			foreach (IUMRecognitionPhrase iumrecognitionPhrase in results)
			{
				string idFromPhrase = ResultWrapper.GetIdFromPhrase(iumrecognitionPhrase);
				if (!dictionary.ContainsKey(idFromPhrase))
				{
					dictionary.Add(idFromPhrase, iumrecognitionPhrase);
				}
			}
			return dictionary;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003AB48 File Offset: 0x00038D48
		internal bool Contains(string key)
		{
			return this.alternateHash.ContainsKey(key);
		}

		// Token: 0x04000A7E RID: 2686
		private int count;

		// Token: 0x04000A7F RID: 2687
		private Dictionary<string, IUMRecognitionPhrase> alternateHash;
	}
}
