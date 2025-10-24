using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SKODE
{
    public class SingleDoozyUIManager<T> : BaseDoozyUIManager where T : BaseDoozyUIManager
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    List<T> ts = DreamRiverObject.Skode_GetTObjs<T>();
                    if (ts.Count > 1)
                    {
                        foreach (var variable in ts)
                        {
                            Debug.Log("场景存在多个" + variable, variable.gameObject);
                        }
                    }

                    _instance = ts.FirstOrDefault(obj => obj != null && obj.gameObject.activeInHierarchy);

                    // 如果没有找到激活的对象，则选择第一个没有激活的对象
                    if (_instance == null && ts.Count > 0)
                    {
                        _instance = ts[0];
                    }
                }

                if (_instance == null)
                {
                    Debug.Log($"{typeof(T).Name} 不存在");
                }

                return _instance;
            }
        }
    }
}