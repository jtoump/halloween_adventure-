import requests
import requests.auth
import pandas as pd
import time
import random





client_auth = requests.auth.HTTPBasicAuth('EVgVgTl4d4PQ9A', '-vnvg9kAkkejwpWFgjpImVphAqs')
post_data = {"grant_type": "password", "username": "dartneige", "password": "196064"}
headers = {"User-Agent": "ChangeMeClient/0.1 by dartneige"}
response = requests.post("https://www.reddit.com/api/v1/access_token", auth=client_auth, data=post_data, headers=headers)
response = response.json()

at=response["access_token"]





def savetodaframe(response, ind=0):
    test = pd.DataFrame(columns=("title","author","low_res","high_res"))
    index=ind
    for i in response['data']['children']:
        # print(index)

        title = i['data']['title']
        author = i['data']['author']
        try:
            url = i['data']['preview']['images'][0]['resolutions'][0]['url']
            url1=i['data']['preview']['images'][0]['source']['url']
        except:
            url = i['data']['url']
            url1=url
        test.loc[index]=[title,author,url,url1]

        index+=1
        after= response['data']['after']

    return test,index,after



def maketherequest(at, after=""):

    headers = {"Authorization": "bearer %s" % (at), "User-Agent": "ChangeMeClient/0.1 by dartneige"}
    response = requests.get("https://oauth.reddit.com/r/EarthPorn/new.json?sort=new&raw_json=1&after=%s"%(after), headers=headers)
    here = response.json()

    return here

after=""
i=0
newindex=0
while i<100:

    out = maketherequest(at,after)
    # print (i)
    checkthis,newindex,after=savetodaframe(out,newindex)
    time.sleep(random.random()*2)
    i+=1
    with open('redditlow.csv', 'a') as f:
        checkthis.to_csv(f, header=False,encoding='utf-8')


# checkthis.to_csv("trythisone.csv", encoding='utf-8', index=False)
# print(checkthis)



# d = {'col1': [1, 2], 'col2': [3, 4]}
# df = pd.DataFrame(data=d)
#
# df.loc[2]=['here',]
# print(df)# print (checkthis)