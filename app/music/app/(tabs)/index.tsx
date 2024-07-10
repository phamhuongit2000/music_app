import React, { useState, useEffect } from 'react';
import { StyleSheet, View, Text, Image, ScrollView, TouchableOpacity } from 'react-native';

import AudioPlayer from '@/components/AudioPlayer';
import { SERVER_DOMAIN } from '../../config';

const App: React.FC = () => {
  const [musicData, setMusicData] = useState<MusicItem[]>([]);
  const [albumsData, setAlbumsData] = useState<AlbumItem[]>([]);
  const [singersData, setSingerData] = useState<SingerItem[]>([]);

  useEffect(() => {
    const fetchMusicData = async () => {
      try {
        const response = await fetch(`${SERVER_DOMAIN}/api/Song/GetAll`);
        const json = await response.json();
        setMusicData(json);
      } catch (error) {
        console.error('Error fetching music data:', error);
      }
    };

    const fetchAlbumsData = async () => {
      try {
        const response = await fetch(`${SERVER_DOMAIN}/api/Album/GetAll`);
        const json = await response.json();
        setAlbumsData(json);
        console.log(json);
        console.log(albumsData);
      } catch (error) {
        console.error('Error fetching music data:', error);
      }
    };

    const fetchSingersData = async () => {
      try {
        const response = await fetch(`${SERVER_DOMAIN}/api/Singer/GetAll`);
        const json = await response.json();
        setSingerData(json);
        console.log(json);
        console.log(singersData);
      } catch (error) {
        console.error('Error fetching music data:', error);
      }
    };

    fetchMusicData();
    fetchAlbumsData();
    fetchSingersData();
  }, []);

  return (
    <ScrollView style={styles.container}>
      <Image
        source={require('../../assets/title.jpg')}
        style={styles.coverImage}
      />
      <View style={styles.coverImageTextContainer}>
        <Text style={styles.coverImageTitle}>BK nhạc</Text>
        <Text style={styles.coverImageSubtitle}>Hãy bắt đầu 1 ngày mới đầy năng lượng</Text>
      </View>

      {/* New Music */}
      <Text style={styles.title}>Bài hát mới</Text>
      <ScrollView horizontal={true} style={styles.horizontalScrollView}>
        {musicData.map((item, index) => (
          <View key={index} style={{ marginRight: 10 }}>
            <AudioPlayer
              url={item.audioUrl}
              img={item.imgUrl}
              name_music={item.name}
              name_singer={item.singerName}
              style={{ width: 100, height: 100, marginRight: 10 }}
            />
            <Text numberOfLines={1} style={styles.newMusicSinger}>{item.name}</Text>
            <Text numberOfLines={1} style={styles.newMusicName}>{item.singerName}</Text>
          </View>
        ))}
      </ScrollView>

      {/* Ablums */}
      <Text style={styles.title}>Ablums</Text>
      <ScrollView horizontal={true} style={styles.horizontalScrollView}>
        {albumsData.map((item, index) => (
          <View key={index} style={{ marginRight: 20 }}>
            <Image 
              source={{ uri: item.imgUrl }}
              style={styles.albumsImg}
            />
            <Text numberOfLines={1} style={styles.albumsTitle}>{item.name}</Text>
          </View>
        ))}
      </ScrollView>

      {/* Nghệ sỹ */}
      <Text style={styles.title}>Ca sỹ</Text>
      <ScrollView horizontal={true} style={styles.horizontalScrollView}>
        {singersData.map((item, index) => (
          <View key={index} style={{ marginRight: 20 }}>
            <Image 
              source={{ uri: item.avatarUrl }} 
              style={styles.singersImg}
            />
            <Text numberOfLines={1} style={styles.singersTitle}>{item.name}</Text>
          </View>
        ))}
      </ScrollView>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  coverImage: {
    width: '100%',
    height: 180,
  },
  coverImageTextContainer: {
    position: 'absolute',
    top: 100,
  },
  coverImageTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    left: 20,
  },
  title: {
    fontSize: 24,
    height: 50,
    fontWeight: 'bold',
    left: 20,
    top: 10,
  },
  coverImageSubtitle: {
    fontSize: 16,
    left: 20,
  },
  horizontalScrollView: {
    paddingHorizontal: 10,
  },
  newMusicSinger: {
    width: 100,
    fontWeight: 'bold',
  },
  newMusicName: {
    width: 100,
  },

  // Ablbums css
  albumsImg: {
    width: 100,
    height: 100,
    borderRadius: 50
  },
  albumsTitle: {
    width: 100,
    fontWeight: 'bold',
  },

  // Singer css
  singersImg: {
    width: 100,
    height: 100,
  },
  singersTitle: {
    width: 100,
    fontWeight: 'bold',
    marginBottom: 20
  },
});

interface MusicItem {
  _id: string;
  audioUrl: string;
  imgUrl: string;
  name: string;
  singerName: string;
  views: string;
  likes: string;
}

interface AlbumItem {
  _id: string;
  name: string;
  description: string;
  imgUrl: string;
}

interface SingerItem {
  _id: string;
  name: string;
  description: string;
  avatarUrl: string;
}

export default App;
