using System.Collections;
using UnityEngine;

namespace LuckyWheels.Code.Services.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}