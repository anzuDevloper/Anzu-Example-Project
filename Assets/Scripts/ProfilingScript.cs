using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine.Profiling;



public class ProfilingScript : MonoBehaviour
{
    //PerformanceCounter cpuCounter;
    //PerformanceCounter ramCounter;

    //void Start()
    //{
    //    cpuCounter = new PerformanceCounter();

    //    cpuCounter.CategoryName = "Processor";
    //    cpuCounter.CounterName = "% Processor Time";
    //    cpuCounter.InstanceName = "_Total";

    //    ramCounter = new PerformanceCounter("Memory", "Available MBytes");
    //}

    //void Update()
    //{
    //    print(getCurrentCpuUsage());
    //    print(getAvailableRAM());
    //}

    //public string getCurrentCpuUsage()
    //{
    //    return cpuCounter.NextValue() + "%";
    //}

    //public string getAvailableRAM()
    //{
    //    return ramCounter.NextValue() + "MB";
    //}
}
