using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSetting.JsonSetting;

namespace DataSetting{
    [System.Serializable]
    public struct ScanData{
        public string angle;
        public string distance;
    }

    [System.Serializable]
    public struct ScanDataArray{
        public ScanData[][] data;
    }

    [System.Serializable]
    public struct ScanData2D{
        public string location;
        public int[][] depth_data;
    }

    /// <summary>
    /// 데이터 관리 클래스
    /// </summary>
    public class DataController : MonoBehaviour
    {
        private string type;        // 데이터 타입
        private string path;        // 데이터 경로
        private DataJson json;      // json 데이터

        public string Type{
            get { return type; }
            set { type = value; }
        }

        public string Path{
            get { return path; }
            set { path = value; }
        }

        public DataController(){
            type = "";
            path = "";
        }

        public DataController(string type, string path){
            this.type = type;
            this.path = path;
        }


        public ScanDataArray LoadData(){
            if(path != ""){
                switch(type){
                    case "json":
                        json = new DataJson(path,false);
                        break;
                    default:
                        break;
                }
                return json.ScanData;
            }
            return new ScanDataArray();
        }


        public ScanData2D LoadData2(){
            if(path != ""){
                switch(type){
                    case "json":
                        json = new DataJson(path,true);
                        break;
                    default:
                        break;
                }
                return json.ScanData2D;
            }
            return new ScanData2D();
        }

        // 데이터 로드
        // data 저장
        // 데이터 수집
        // 데이터 관리
    }

}

