using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMgr : MonoBehaviour
{
    private Transform groupParent; // 그룹의 부모 Transform
    private List<GameObject> groups; // 그룹들을 저장하기 위한 리스트

    public GroupMgr(Transform parent)
    {
        groupParent = parent;               // 그룹부모의 위치값을 세팅
        groups = new List<GameObject>();    
    }


    /// <summary>
    /// 인수로 받아온 그룹이름을 가진 그룹을 생성하고 부모의 자식 객체로 세팅후 반환
    /// </summary>
    /// <param name="groupName"> 그룹의 이름 </param>
    /// <returns>그룹 객체를 반환</returns>
    public GameObject createGroup(string groupName)
    {
        GameObject group = new GameObject(groupName);
        Transform groupTransform = group.transform;
        groupTransform.SetParent(groupParent);
        groups.Add(group);
        return group;
    }

    /// <summary>
    /// 그룹에 객체를 추가하는 메소드
    /// </summary>
    /// <param name="group">객체를 담을 그룹 객체</param>
    /// <param name="obj">객체</param>
    public void addObjectToGroup(GameObject group, GameObject obj)
    {
        obj.transform.SetParent(group.transform);
    }

    /// <summary>
    /// 그룹의 위치를 설정하는 메서드
    /// </summary>
    /// <param name="group">위치를 변경할 그룹</param>
    /// <param name="position">위치 값</param>
    public void setGroupPosition(GameObject group, Vector3 position)
    {
        group.transform.position = position;
    }

    /// <summary>
    /// 그룹의 이름을 통해서 그룹 찾기
    /// </summary>
    /// <param name="groupName">그룹이름</param>
    /// <returns>해당 그룹을 반환</returns>
    public GameObject findGroup(string groupName)
    {
        return groups.Find(group => group.name == groupName);
    }

    /// <summary>
    /// 전달 받은 그룹 객체를 리스트에서 제거 및 삭제
    /// </summary>
    /// <param name="group"></param>
    public void deleteGroup(GameObject group)
    {
        groups.Remove(group);
        Destroy(group);
    }


    /// <summary>
    /// 모든 그룹을 삭제하는 메서드
    /// </summary>
    public void deleteAllGroups()
    {
        foreach (GameObject group in groups)
        {
            Destroy(group);
        }
        groups.Clear();
    }
}
